function EitherOr(value, fallback) {
    if (typeof value === 'undefined' || value == null) {
        return fallback;
    }

    return value;
}

const path = require('path');
const VueLoader = require('vue-loader');
const Webpack = require('webpack');

const output = EitherOr(process.env.npm_config_output, path.join(__dirname, './wwwroot/dist'));
const mode = EitherOr(process.env.npm_config_mode, 'development');

module.exports = {
    mode: mode,
    entry: {
        main: './Scripts/app.js'
    },
    output: {
        path: output,
        filename: '[name].js',
        publicPath: 'dist/'
    },
    resolve: {
        alias: {
            vue: 'vue/dist/vue.js'
        }
    },
    module: {
        rules: [
            { test: /\.vue\.html$/, include: /Scripts/, loader: 'vue-loader', options: { loaders: { js: 'awesome-typescript-loader?silent=true' } } }
        ]
    },
    plugins: [
        new VueLoader.VueLoaderPlugin()
    ]
};