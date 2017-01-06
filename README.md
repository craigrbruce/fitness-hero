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


### Prerequisites

* OS X, Windows or Linux
* [Node.js](https://nodejs.org) v6 or newer
* [.NET Core](https://www.microsoft.com/net/core) and [.NET Core SDK](https://www.microsoft.com/net/core)
* [Visual Studio Code](https://code.visualstudio.com/) with [C# extension](https://github.com/OmniSharp/omnisharp-vscode) (or Visual Studio 2015 or newer)


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


### How to Deploy

Before you can deploy your app to [Azure App Service](https://azure.microsoft.com/services/app-service/),
you need to open Web App settings in [Azure Portal](https://portal.azure.com/), go to "Deployment
Source", select "Local Git Repository" and hit [OK]. Then copy and paste "Git clone URL" of your
Web App into [`run.js/publish`](run.js) file. Finally, whenever you need to compile your
app into a distributable format and upload that to Windows Azure App Service, simply run:

```shell
$ node run publish              # Same as running: npm run publish
```


### Related Projects

* [React App SDK](https://github.com/kriasoft/react-app) — build React applications with a single dev dependency and no build configuration
* [React Starter Kit](https://github.com/kriasoft/react-starter-kit) — Isomorphic web app boilerplate (Node.js, Express, GraphQL, React)
* [Babel Starter Kit](https://github.com/kriasoft/babel-starter-kit) — JavaScript library boilerplate (ES2015+, Babel, Rollup)
* [ASP.NET Core Starter Kit `|>` F#](https://github.com/kriasoft/fsharp-starter-kit) — Web app boilerplate (F#, .NET Core, Kestrel, GraphQL, React)
* [Universal Router](https://github.com/kriasoft/universal-router) — Isomorphic router for web and single-page applications (SPA)
* [Membership Database](https://github.com/membership/membership.db) — SQL database boilerplate for web app users, roles and auth tokens
