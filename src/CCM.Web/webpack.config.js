var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var path = require('path');
var webpack = require('webpack');
var { AureliaPlugin } = require('aurelia-webpack-plugin');



var bundleOutputDir = './wwwroot/spadist';
module.exports = {
    resolve: { extensions: ['.js', '.ts'], modules: ["ClientApp", "node_modules"] },
    entry: { app: 'aurelia-bootstrapper' }, // Note: The aurelia-webpack-plugin will add your app's modules to this bundle automatically
    output: {
        path: path.resolve(bundleOutputDir),
        publicPath: '/spadist/',
        filename: '[name].js'
    },
    module: {
        loaders: [
            { test: /\.ts$/, include: /ClientApp/, loader: 'ts-loader', query: { silent: true } },
            { test: /\.html$/, loader: 'html-loader' },
            { test: /\.css$/i, use: ['style-loader', 'css-loader'], issuer: /\.[tj]s$/i },
            { test: /\.css$/i, use: 'css-loader', issuer: /\.html?$/i },
            { test: /\.(jpg|png|woff|woff2|eot|ttf|svg)$/, loader: 'url-loader?limit=100000' },
            { test: /\.json$/, loader: 'json-loader' },
            {
                test: /\.scss$/,
                use: ['css-loader', 'sass-loader?precision=10']
            }              
        ]
    },
    plugins: [
        new webpack.DefinePlugin({ IS_DEV_BUILD: JSON.stringify(isDevBuild) }),
        new webpack.DllReferencePlugin({
            context: __dirname,
            manifest: require('./wwwroot/vendor/vendor-manifest.json')
        }),        
        new AureliaPlugin({
            aureliaApp: "boot"
        })
    ].concat(isDevBuild ? [
        // Plugins that apply in development builds only
        new webpack.SourceMapDevToolPlugin({
            filename: '[file].map', // Remove this line if you prefer inline source maps
            moduleFilenameTemplate: path.relative(bundleOutputDir, '[resourcePath]') // Point sourcemap entries to the original file locations on disk
        })
    ] : [
            // Plugins that apply in production builds only
            new webpack.optimize.UglifyJsPlugin()
        ])
};
