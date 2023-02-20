/** 获取当前模块信息 */
//const pack = require('../package.json')
const packs = import.meta.globEager("../package.json")
let pack = {}
Object.keys(packs).forEach(item => {
    pack = packs[item]
});
console.log('pack', pack)
export default {
    id: pack.id,
    name: pack.label,
    code: 'admin',
    version: pack.version,
    description: pack.description
}
