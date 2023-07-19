window端
用vscode打開shorturltest專案
開啟vs的terminal
$ pip freeze > requirements.txt
使用docker套件生成dockerfile
port設定13501
傳輸到ubuntu
ubuntu端
打開shorturl專案的terminal
$ sudo docker build -t shorturltest .
$ sudo docker run -d --name shorturltest -p 12501:13501 shorturltest
$ sudo docker ps
window端
用vscode打開ChannelProgramtest專案
使用docker套件生成dockerfile
port設定13502
dockerfile的位置要放在跟專案資料夾跟套件資料夾一樣的層
傳輸到ubuntu
ubuntu端
打開專案ChannelProgramtest的terminal
$ sudo docker build -t channelprogramtest .
$ sudo docker run -d --name channelprogramtest -p 12502:13502 channelprogramtest
停止並刪除上面兩個專案的container
$ sudo docker stop shorturltest
$ sudo docker rm shorturltest
$ sudo docker stop channelprogramtest
$ sudo docker rm channelprogramtest
上傳兩個Image到dockerhub
$ sudo docker login
dockerhub網站，創建shorturltest、channelprogramtest
$ sudo docker tag shorturltest zihrueiliou/shorturltest:v1.0.0
$ sudo docker push zihrueiliou/shorturltest:v1.0.0
$ sudo docker tag channelprogramtest zihrueiliou/channelprogramtest:v1.0.0
$ sudo docker pushzihrueiliou/channelprogramtest:v1.0.0
$ sudo docker images
刪除上面兩個專案的image
$ sudo docker rmi shorturltest
$ sudo docker rmi zihrueiliou/shorturltest:v1.0.0
$ sudo docker rmi channelprogramtest
$ sudo docker rmi zihrueiliou/channelprogramtest:v1.0.0
拉dockerhub的image
$ sudo docker pull zihrueiliou/shorturltest:v1.0.0
$ sudo docker pull zihrueiliou/channelprogramtest:v1.0.0
$ sudo docker images
同步到volume
$ sudo docker run -it -d -p 12501:13501 -v shorturltest:/app --name shorturltest zihrueiliou/shorturltest:v1.0.0
$ sudo docker run -it -d -p 12502:35898 -v channelprogramtest:/app --name channelprogramtest zihrueiliou/channelprogramtest:v1.0.0
nginx
$ sudo docker run -d --name nginxtest -v nginxtest:/etc/nginx --net=host nginx:latest
進入nginx的volume
編輯default.conf
新增ssl資料夾
在ssl資料夾新增2023資料夾
在裡面放入ssl
重啟nginx
$ sudo docker stop nginxtest
$ sudo docker start nginxtest
關閉專案docker
$ sudo docker stop shorturltest
$ sudo docker rm shorturltest
$ sudo docker stop channelprogramtest
$ sudo docker rm channelprogramtest
yml操作
$ sudo docker-compose build
$ sudo docker-compose push
$ sudo docker-compose up -d
