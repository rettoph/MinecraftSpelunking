import typescript from '@rollup/plugin-typescript';

/** @type {import('rollup').RollupOptions} */
const options = {
    input: "Scripts/app.ts",
    output: {
        file: "./wwwroot/dist/bundle.js",
    },
    plugins: [
        typescript()
    ]
}

export default options