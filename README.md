# Fitness Hero Project

### Directory Layout

```shell
.
├── /.vscode/                   # Visual Studio Code settings
├── /build/                     # The folder for compiled output
├── /client/                    # Client-side app (frontend)
│   ├── /components/            # Common or shared UI components
│   ├── /utils/                 # Helper functions and utility classes
│   ├── /views/                 # UI components for web pages (screens)
│   ├── history.js              # HTML5 History API wrapper used for navigation
│   ├── main.js                 # Entry point that bootstraps the app
│   ├── router.js               # Lightweight application router
│   ├── routes.json             # The list of application routes
│   └── store.js                # Application state manager (Redux)
├── /client.test/               # Unit and integration tests for the frontend app
├── /docs/                      # Documentation to the project
├── /public/                    # Static files such as favicon.ico etc.
│   ├── robots.txt              # Instructions for search engine crawlers
│   └── ...                     # etc.
├── /server/                    # Web server and data API (backend)
│   ├── /Controllers/           # ASP.NET Web API and MVC controllers
│   ├── /Models/                # Entity Framework models (entities)
│   ├── /Views/                 # Server-side rendered views
│   ├── appsettings.json        # Server-side application settings
│   ├── Startup.cs              # Server-side application entry point
│   └── web.config              # Web server settings for IIS
├── /server.test/               # Unit and integration tests for the backend app
│── jsconfig.json               # Visual Studio Code settings for JavaScript
│── package.json                # The list of project dependencies and NPM scripts
│── run.js                      # Build automation script (similar to gulpfile.js)
└── webpack.config.js           # Bundling and optimization settings for Webpack
```



### Getting Started

**Step 1**. Clone the latest version of **ASP.NET Core Starter Kit** on your local machine by running:

```shell
$ git clone -o aspnet-starter-kit -b master --single-branch \
      https://github.com/kriasoft/aspnet-starter-kit.git MyApp
$ cd MyApp
```


**Step 2**. Install project dependencies listed in [`project.json`](server/project.json) and
[`package.json`](package.json) files: 

```shell
$ npm install                   # Install both Node.js and .NET Core dependencies
```

**Step 3**. Finally, launch your web app:

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
