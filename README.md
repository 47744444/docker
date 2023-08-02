# Python專案
## main.py
```
from flask import Flask, render_template, request
import os

app = Flask(__name__)

app.config['UPLOAD_FOLDER'] = 'static/data'

@app.route('/', methods=['GET', 'POST'])
def index():
    if request.method == 'POST':
        # 检查是否有文件被上传
        if 'file' not in request.files:
            return 'No file part'
        
        file = request.files['file']
        
        # 如果用户没有选择文件，浏览器也会发送一个空的part
        if file.filename == '':
            return 'No selected file'
        
        # 将上传的文件保存到指定目录中
        file.save(os.path.join(app.config['UPLOAD_FOLDER'], file.filename))
        
        return 'File uploaded successfully!'
    else:
        return render_template('index.html')


if __name__ == '__main__':
    app.debug = True
    app.run()
```
## index.html
```
<!DOCTYPE html>
<html>
<head>
    <title>File Upload</title>
</head>
<body>
    <h1>Upload a File</h1>
    <form method="POST" enctype="multipart/form-data">
        <input type="file" name="file">
        <input type="submit" value="Upload">
    </form>
</body>
</html>
```
## DockerFile
```
FROM python:3.9-slim

# 指定 Image 中的工作目錄
WORKDIR /code

# 將 Dockerfile 所在目錄下的所有檔案複製到 Image 的工作目錄 /code 底下
ADD . /code

# 在 Image 中執行的指令：安裝 requirements.txt 中所指定的 dependencies
RUN pip install -r requirements.txt

# Container 啟動指令：Container 啟動後通過 python 運行 main.py
CMD ["python", "./main.py"]
```
## 資料結構
```
- your_project_folder/
    - main.py
    - templates/
        - index.html
    - static/
        - data/
    - Dockerfile
```
# 打包Docker
## 安裝Docker
### Visit https://docs.docker.com/engine/install/ubuntu/
## 發布image
```
#在dockerfile目錄下建置
docker build -t <dockerhub/imagename:版本> .
docker images
```
## 推送至dockerhub
```
docker push <dockerhub/imagename:版本>
```
## 創建volume
```
sudo docker volume create <VolName>
```
## 執行container
```
docker run -d --name <dockerhub/imagename:版本> -v <VolName>:/code -p 5000:5000 <dockerhub/imagename:版本>
docker ps
```
# nginx設定
## 執行nginx的Image
```
docker run -d --name nginx --net=host nginx:latest 
```
## 進入nginx容器
```
docker exec -it nginx /bin/bash
```
## 安裝必要套件
```
apt update
apt install nano
```
## 進到default目錄下
```
cd /etc/nginx/conf.d 
```
## 修改default
```
nano default.conf
#在service底下加入
location ^~/upload/ {
    proxy_pass        http:127.0.0.1:5000/;
}
```
## 最後打開 http:localhost/upload
