const path = require('path');
const webpack = require('webpack');

var config = {
    module: {
        rules: [
            {
                test: /\.css$/,
                use: [{ loader: 'style-loader' }, { loader: 'css-loader' }]
            },
            {
                test: /\.png$/,
                loader: 'url-loader?limit=100000'
            },
            {
                test: /\.woff(2)?(\?v=[0-9]\.[0-9]\.[0-9])?$/,
                loader: 'url-loader?limit=10000&mimetype=application/font-woff'
            },
            {
                test: /\.(ttf|otf|eot|svg)(\?v=[0-9]\.[0-9]\.[0-9])?|(jpg|gif)$/,
                loader: 'file-loader'
            }
        ]
    }
};

var bundlemain = Object.assign({}, config, {
    name: "bundle-main",
    entry: "./bundles/bundle-main.js",
    output: {
        path: path.resolve(__dirname, 'wwwroot/lib'),
        filename: "bundle-main.js"
    },
    plugins: [
        new webpack.ProvidePlugin({
            $: 'jquery',
            jQuery: 'jquery'
        }),
        new webpack.ProvidePlugin({
            $: 'bootstrap',
            bootstrap: 'bootstrap'
        })
    ]
});

var bundlevalidation = Object.assign({}, config, {
    name: "bundle-validation",
    entry: "./bundles/bundle-validation.js",
    output: {
        path: path.resolve(__dirname, 'wwwroot/lib'),
        filename: "bundle-validation.js"
    }
});


module.exports = [
    bundlemain, bundlevalidation
];