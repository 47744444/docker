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
# index.html
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
# DockerFile
```
# For more information, please refer to https://aka.ms/vscode-docker-python
FROM python:3.10-slim

EXPOSE 5000

# Keeps Python from generating .pyc files in the container
ENV PYTHONDONTWRITEBYTECODE=1

# Turns off buffering for easier container logging
ENV PYTHONUNBUFFERED=1

# Install pip requirements
COPY requirements.txt .
RUN python -m pip install -r requirements.txt

WORKDIR /app
COPY . /app

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-python-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

# During debugging, this entry point will be overridden. For more information, please refer to https://aka.ms/vscode-docker-python-debug
CMD ["gunicorn", "--bind", "0.0.0.0:5000", "main:app"]
```
# 資料結構
```
- your_project_folder/
    - main.py
    - templates/
        - index.html
    - static/
        - data/
    - Dockerfile
```
# 安裝Docker
## Visit https://docs.docker.com/engine/install/ubuntu/
# 發布image
```
#在dockerfile目錄下建置
docker build -t <dockerhub/imagename:版本> .
docker images
```
# 推送至dockerhub
```
docker push <dockerhub/imagename:版本>
```
# 創建volume
```

```
# 執行container
```
docker run -d --name <dockerhub/imagename:版本> -p 5000:5000 <dockerhub/imagename:版本>
docker ps
```
