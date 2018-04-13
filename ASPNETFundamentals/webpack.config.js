var path = require('path');

module.exports = {
    context: path.join(__dirname, 'Content'),
    entry: {
        client: './client'
    },
    output: {
        path: path.join(__dirname, 'wwwroot/build'),
        filename: '[name].bundle.js'
    },
    module: {
        loaders: [
            { test: /\.js$/, loader: 'babel-loader', exclude: /node_modules/ },
            { test: /\.jsx$/, loader: 'babel-loader', exclude: /node_modules/ }
        ]
    },
    resolve: {
        // Allow require('./blah') to require blah.jsx
        extensions: ['.js', '.jsx']
    }
}