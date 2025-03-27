// webpack.config.js
module.exports = {
    // ... existing config
    module: {
      rules: [
        {
          test: /pdf\.worker\.js$/,
          use: 'file-loader'
        }
      ]
    }
  }