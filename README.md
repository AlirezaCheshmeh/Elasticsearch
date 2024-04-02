docker pull elasticsearch:8.13.0
docker pull kibana:8.13.0



 docker run -d --name elasticsearch -p 9200:9200 -e "discovery.type=single-node" -e "ELASTIC_PASSWORD=yourStrongPassWord" elasticsearch:8.13.0
 docker run -d --name kibana -p 5601:5601 kibana:8.13.0
 
--name: your desired container name
-d:     run contianer in backgroung and print container ID
-p :    define your port    external port : portin in container
-e :    set environmnet vatiable 

use docker ps -a to view a list of all containers


kibana is up in http://localhost:5601
for configur with your elasticsearch
docker exec -it <elasticsearch container id > /bin/bash
/bin/elasticsearch-create-enrollment-token --scope kibana
  copy and paste token suitble place :D
  
for generation otp code
docker exec -it 
/bin/kibana-verification-code 

last step enter user and pass 
user = elastic (default user)
pass = yourStringPassWord (when docker run ... -e "ELASTIC_PASSWORD=....")
 

 
 
