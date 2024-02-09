let options = import('./rollup.config.development.js');

if (process.env.NODE_ENV === 'production') {
    options = import('./rollup.config.production.js');
}

export default options;