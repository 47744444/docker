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
