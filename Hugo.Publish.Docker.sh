# 停止容器
docker stop ApiContainer
# 删除容器
docker rm ApiContainer
# 删除镜像
docker rmi Hugo/ApiImg
# 切换目录
cd /home/Hugo
# 发布项目
./Hugo.Publish.Linux.sh
# 进入目录
cd /home/Hugo/Publish
# 编译镜像
docker build -t Hugo/ApiImg .
# 生成容器
docker run --name=ApiContainer -d -v /etc/localtime:/etc/localtime -it -p 8081:8081 Hugo/ApiImg
# 启动容器
docker start ApiContainer
