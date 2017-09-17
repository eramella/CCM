var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var extractCSS = new ExtractTextPlugin('vendor.css');
const { AureliaPlugin } = require('aurelia-webpack-plugin');
const ProvidePlugin = require('webpack/lib/ProvidePlugin');
const ContextReplacementPlugin = require('webpack/lib/ContextReplacementPlugin')


module.exports = {
    resolve: {
        extensions: ['.js']
    },
    module: {
        rules: [
            { test: /\.(jpg|png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
            { test: /\.css(\?|$)/, use: extractCSS.extract(['css-loader']) },
            { test: /\.html$/, loader: 'html-loader' },
            {
                test: /\.scss$/,
                use: extractCSS.extract({
                    use: ['css-loader', 'sass-loader?precision=10'],
                    fallback: 'style-loader'
                })
            }
        ]
    },
    entry: {
        vendor: [
            'aurelia-event-aggregator',
            'aurelia-fetch-client',
            'aurelia-http-client',
            'aurelia-framework',
            'aurelia-history-browser',
            'aurelia-logging-console',
            'aurelia-pal-browser',
            'aurelia-polyfills',
            'aurelia-route-recognizer',
            'aurelia-router',
            'aurelia-templating-binding',
            'aurelia-templating-resources',
            'aurelia-templating-router',
            'aurelia-validation',
            'bootstrap-sass',
            'jquery',
            'moment',
            'aurelia-dialog',
            'toastr',
            'aurelia-bootstrap-datetimepicker',
            'quill',
            'typeahead.js',
            'bootstrap-tagsinput'
        ],
    },
    output: {
        path: path.join(__dirname, 'wwwroot', 'vendor'),
        publicPath: '/vendor/',
        filename: '[name].js',
        library: '[name]_[hash]',
    },
    plugins: [
        extractCSS,
        new ContextReplacementPlugin(/moment[\/\\]locale$/, /en|fr/),
        new webpack.ProvidePlugin({
            $: "jquery",
            jQuery: "jquery",
            jquery: "jquery"
            //'window.jQuery': 'jquery',
            //'window.Tether': 'tether',
            //Tether: 'tether'
        }), // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)
        new webpack.DllPlugin({
            path: path.join(__dirname, 'wwwroot', 'vendor', '[name]-manifest.json'),
            name: '[name]_[hash]'
        }),
        new AureliaPlugin({
            aureliaApp: undefined
        })
    ].concat(isDevBuild ? [] : [
        new webpack.optimize.UglifyJsPlugin({ compress: { warnings: false } })
    ]),
    resolve: {
        alias: {
            // Force all modules to use the same jquery version.
            'jquery': path.join(__dirname, 'node_modules/jquery/src/jquery')
        }
    }
};