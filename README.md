# Fitness Hero

## Where heroes get fit

Launch the app:

```shell
$ node run                      # Compile and lanch the app, same as running: npm start
```

The app should become available at [http://localhost:5000/](http://localhost:5000/).
See [`run.js`](run.js) for other available commands such as `node run build`, `node run publish` etc.
You can also run your app in a release (production) mode by running `node run --release`, or without
Hot Module Replacement (HMR) by running `node run --no-hmr`.


### Set up Postgres DB

#### Run Postgres in Docker container
Note environment variables are the same for the Knexfile development configuration.
```shell
docker run --name postgres-db -e POSTGRES_USER=dbuser -e POSTGRES_PASSWORD=password -e POSTGRES_DB=ebdb -p5432:5432 -d postgres:latest
```

Once the above container is running, you can use psql to interact with the database using the following.
```shell
docker run -it --rm --link postgres-db:postgres postgres psql -h postgres -U dbuser ebdb
```

#### Run Postgres.app
If you can't use Docker and you have a Mac, then install Postgres.app and run the following.
```shell
"/Applications/Postgres.app/Contents/Versions/9.6/bin/psql" -p5432 -d "postgres"
CREATE USER dbuser WITH CREATEDB PASSWORD 'password';
\q
"/Applications/Postgres.app/Contents/Versions/9.6/bin/psql" -p5432 -d "postgres" --username=dbuser --password
CREATE DATABASE ebdb WITH ENCODING='UTF8' OWNER=dbuser;
\q
```


## AWS Beanstalk

### To view environment variables in an EC2 instance
```bash
eb ssh
sudo /opt/elasticbeanstalk/bin/get-config environment --output YAML
