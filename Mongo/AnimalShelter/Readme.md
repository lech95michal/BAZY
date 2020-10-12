## Projekt z rozproszonych baz danych  

Instrukcja uruchomieniowa

```
sudo docker network create my-cluster

sudo docker run --name mongo -p 27017:27017 mongo
sudo docker run --name node1 -d -p 50001:27017 --net my-cluster mongo mongod --replSet "rs"
sudo docker run --name node2 -d -p 50002:27017 --net my-cluster mongo mongod --replSet "rs"

sudo docker exec -it node1 mongo

configuration = {
      "_id" : "rs",
      "members" : [
          {
              "_id" : 0,
              "host" : "node1:27017"
          },
          {
              "_id" : 1,
              "host" : "node2:27017"
          }
      ]
  }

rs.initiate(configuration)

sudo docker build -t animal-shelter .
sudo docker run --net my-cluster animal-shelter
```
