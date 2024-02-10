import typescript from '@rollup/plugin-typescript';
import scss from 'rollup-plugin-scss'

/** @type {import('rollup').RollupOptions} */
const options = {
    input: "Scripts/app.ts",
    output: {
        file: "./wwwroot/dist/bundle.js",
    },
    plugins: [
        typescript(),
        new scss("./wwwroot/dist/bundle.css")
    ]
}

export default options