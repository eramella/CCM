var isDevBuild = process.argv.indexOf('--env.prod') < 0;
var path = require('path');
var webpack = require('webpack');
var ExtractTextPlugin = require('extract-text-webpack-plugin');
var extractCSS = new ExtractTextPlugin('../css/main.css');

module.exports = {
    context: path.resolve('PagesSrc'),
    entry: {
        homePage: './homePage.js',
        authPages: './authPages.js',
        profilePage: './profilePage.js'
    },
    output: {
        path: path.join(__dirname, 'wwwroot/pagedist/'),
        publicPath: '/pagedist/',
        filename: '[name].js'
    },
    module: {
        rules: [
            { test: /\.(jpg|png|woff|woff2|eot|ttf|svg)(\?|$)/, use: 'url-loader?limit=100000' },
            {
                test: /\.css(\?|$)/,
                use: extractCSS.extract({
                    fallback: 'style-loader',
                    use: ['css-loader']
                })
            },
            {
                test: /\.scss$/,
                exclude: /node_modules/,
                use: extractCSS.extract({
                    use: ['css-loader', 'sass-loader?precision=10'],
                    fallback: 'style-loader'
                })
            }
        ]
    },
    plugins: [
        extractCSS,
        new webpack.ProvidePlugin({ $: 'jquery', jQuery: 'jquery', jquery: 'jquery' }) // Maps these identifiers to the jQuery package (because Bootstrap expects it to be a global variable)

    ].concat(isDevBuild ? [] : [
        new webpack.optimize.UglifyJsPlugin({ compress: { warnings: false } })
    ])
};
