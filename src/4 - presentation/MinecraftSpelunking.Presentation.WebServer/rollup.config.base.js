import typescript from '@rollup/plugin-typescript';
import scss from 'rollup-plugin-scss'
import node from '@rollup/plugin-node-resolve'
import copy from 'rollup-plugin-copy'

/** @type {import('rollup').RollupOptions} */
const options = {
    input: "Scripts/app.ts",
    output: {
        file: "./wwwroot/dist/bundle.js",
    },
    plugins: [
        typescript(),
        new scss("./wwwroot/dist/bundle.css"),
        node(),
        copy({
            targets: [
                { src: "./node_modules/bootstrap-icons/font/fonts/*", dest: './wwwroot/dist/fonts' }
            ]
        })
    ]
}

export default options