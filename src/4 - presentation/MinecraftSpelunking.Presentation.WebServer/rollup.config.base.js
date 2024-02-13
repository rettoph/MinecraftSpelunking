import typescript from '@rollup/plugin-typescript';
import scss from 'rollup-plugin-scss'
import node from '@rollup/plugin-node-resolve'

/** @type {import('rollup').RollupOptions} */
const options = {
    input: "Scripts/app.ts",
    output: {
        file: "./wwwroot/dist/bundle.js",
    },
    plugins: [
        typescript(),
        new scss("./wwwroot/dist/bundle.css"),
        node()
    ]
}

export default options