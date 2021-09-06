var __defProp=Object.defineProperty,__getOwnPropSymbols=Object.getOwnPropertySymbols,__hasOwnProp=Object.prototype.hasOwnProperty,__propIsEnum=Object.prototype.propertyIsEnumerable,__defNormalProp=(t,e,s)=>e in t?__defProp(t,e,{enumerable:!0,configurable:!0,writable:!0,value:s}):t[e]=s,__spreadValues=(t,e)=>{for(var s in e||(e={}))__hasOwnProp.call(e,s)&&__defNormalProp(t,s,e[s]);if(__getOwnPropSymbols)for(var s of __getOwnPropSymbols(e))__propIsEnum.call(e,s)&&__defNormalProp(t,s,e[s]);return t};!function(t){"function"==typeof define&&define.amd?define(t):t()}((function(){"use strict";class t{constructor(t){this.ctx=t}async load(t){return await this.ctx.http.fetch({method:"GET",url:`[WF]${t}`})}async loadSettings(t){if(t){this.ctx.config.addSetting("[settingsUrl]",t);const e=await this.ctx.http.fetch({method:"GET",url:t});Object.keys(e).forEach((t=>this.ctx.config.addSetting(t,e[t])))}}}class e{constructor(){this.settings=new Map}getSetting(t){return this.settings.get(t)}addSetting(t,e){-1===t.indexOf("[")&&(t=`[${t}]`),this.settings.set(t,e)}}var s,i;(i=s||(s={})).START_LOADING="START_LOADING",i.END_LOADING="END_LOADING",i.UN_AUTHORIZED="UN_AUTHORIZED";class n{constructor(t,e){this.config=t,this.message=e}async fetch(t){var e;try{this.message.post({type:s.START_LOADING});const i=await fetch(this.resolveSetting(t.url),this.getConfig(t));if(i.status>=400){const e=await i.json();throw i.status>=401&&this.message.post({type:s.UN_AUTHORIZED,metadata:{endpoint:t,error:e}}),{code:i.status,message:i.statusText,error:e}}return(null==(e=i.headers.get("content-type"))?void 0:e.indexOf("json"))?await i.json():await i.text()}finally{setTimeout((()=>this.message.post({type:s.END_LOADING})))}}getConfig(t){return{method:t.method,mode:"cors",headers:Object.apply({"Content-Type":"application/json"},t.headers),redirect:"follow",referrer:"no-referrer",body:t.body?JSON.stringify(t.body):null}}resolveSetting(t,e=0){if(e>2)return t;const s=t.match(/\[[\w|_]+\]/g);if(!s)return t;let i=s.reduce(this.replace.bind(this),t);return i.indexOf("[")>-1&&(i=this.resolveSetting(i,e++)),i}replace(t,e){let s=this.config.getSetting(e);return s&&s.indexOf("[SELF]")>-1?s.replace("[SELF]",t.replace(e,"")):t.replace(e,s)}}class r{constructor(t,e){this.config=t,this.pipes=e,this.model={},this.sessionId=this.UUID()}getValue(t,e=this.model){if(0===t.indexOf("[")||t.indexOf("]")===t.length-1)return this.config.getSetting(t);const s=t.split(".").reduce(((t,e)=>t?t[e]:void 0),__spreadValues({},e));return t.match(/([a-z|A-Z]+\.[a-z|A-Z]+)+/g)||void 0!==s?s:t}getInterpolatedValue(t){if(!t)return t;const e=t.match(/\{\{\[*(?:(\w|\.|\||-)+)\]*\}\}/g);return e&&0!==e.length?e.reduce(((t,e)=>this.replaceAll(t,e)),t):t}setValue(t,e){0===t.indexOf("[")||t.indexOf("]")===t.length-1?this.config.addSetting(t,e):this.model=this.merge(this.model,t,e)}save(){sessionStorage.setItem(this.sessionId,JSON.stringify(this.model))}load(){const t=sessionStorage.getItem(this.sessionId);this.clearCache(),t&&(this.model=JSON.parse(t))}clearCache(){sessionStorage.clear()}merge(t,e,s){if(!e)return;let i=__spreadValues({},t);return e.split(".").reduce(((t,e,i,n)=>(t[e]=i==n.length-1?s:__spreadValues({},t[e]),t[e])),i),i}UUID(){return"xxxxxxxxRxxxxR4xxxRyxxxRxxxxxxxxxxxx".replace(/[xy]/g,(function(t){var e=16*Math.random()|0;return("x"==t?e:3&e|8).toString(16)}))}replaceAll(t,e){const s=e.substring(2,e.length-2).split("|"),i=s.slice(2);let n=this.getValue(s[0]);return(null==s?void 0:s.length)>1&&this.pipes[s[1]]&&(n=this.pipes[s[1]](n,i)),t.replace(e,n)}}class o{constructor(){this.type=""}async execute(t){}async exit(t){return!0}}class a extends o{constructor(t){super(),this.activity=t,this.type="not-found-activity"}async execute(t){console.error(`The ${this.activity} was not found!!`)}}class c{constructor(t,e,s){this.ctx=t,this.wfLoader=e,this.activities=s}async goto(t,e=""){await this.loadProcess(e),await this.loadActivity(t)}async loadProcess(t){t.length>0&&(this.wf=await this.wfLoader.load(t))}async loadActivity(t){var e,s,i;if(void 0!==this.act&&!this.act.exit(this.ctx))return;const n=null==(s=null==(e=this.wf)?void 0:e.activities)?void 0:s.find((e=>e.name===t)),r=null!=(i=this.activities.find((t=>t.type===(null==n?void 0:n.type))))?i:new a(t);this.act=Object.assign(r,n),await this.act.execute(this.ctx)}}class l extends o{constructor(){super(...arguments),this.type="page-activity",this.controls=[]}async execute(t){t.container.innerHTML="",this.controls.forEach(this.createElement.bind(this,t,t.container))}async exit(t){return Array.from(t.container.querySelectorAll("[id]")).forEach((e=>t.model.setValue(e.id,e.value))),!0}createElement(t,e,s){var i,n;const r=Object.assign(document.createElement(s.tag),s,{ctx:t});s.id&&(r.value=null!=(i=t.model.getValue(s.id))?i:""),this.bindCaption(t,r,s),this.bindEvent(t,r,s),e.appendChild(r),null==(n=s.controls)||n.forEach(this.createElement.bind(this,t,r))}bindCaption(t,e,s){this.interpolate(t,"caption",e,s),this.interpolate(t,"textContent",e,s),this.interpolate(t,"innerHTML",e,s)}interpolate(t,e,s,i){s[e]&&(s[e]=t.model.getInterpolatedValue(i[e]||s[e]))}bindEvent(t,e,s){Object.keys(s).filter((t=>t.startsWith("on"))).forEach((i=>e[i.toLowerCase()]=e=>{e.preventDefault(),new Function("ctx",s[i])(t)}))}}class h extends o{constructor(){super(...arguments),this.type="api-activity"}async execute(t){await this.callEndpoints(t),this.next&&t.wf.goto(this.next)}async callEndpoints(t){if(!this.endpoints)return!1;for(const e of this.endpoints)e.body=this.getBody(t,e),await this.callEndpoint(t,e);return!0}getBody(t,e){if("GET"===e.method.toUpperCase()||"DELETE"===e.method.toUpperCase())return null;const s=e.mappings;let i={};return s.filter((t=>"out"===t.direction||"inout"===t.direction)).forEach((e=>Object.assign(i,{[e.remote]:t.model.getValue(e.client)}))),i}callEndpoint(t,e){return t.http.fetch(e).then((s=>this.setModel(t,e,s)))}setModel(t,e,s){const i=e.mappings;if(!i||0===i.length)return Object.keys(s).forEach((e=>t.model.setValue(e,s[e])));i.filter((t=>"in"===t.direction||"inout"===t.direction)).forEach((e=>t.model.setValue(e.client,t.model.getValue(e.remote,s))))}}class d{}class u{constructor(t){this.element=t}post(t){this.element.dispatchEvent(new CustomEvent("workflow_changed",{detail:t}))}}class p extends HTMLElement{constructor(){super(...arguments),this.isInitialized=!1,this.ctx=this,this.wfLoader=new t(this),this.activities=[new l,new h],this.pipes=new d,this.config=new e,this.message=new u(this),this.http=new n(this.config,this.message),this.model=new r(this.config,this.pipes),this.wf=new c(this.ctx,this.wfLoader,this.activities)}static get observedAttributes(){return["process"]}async connectedCallback(){this.createContainer(),await this.wfLoader.loadSettings(this.getAttribute("url"));const t=this.getAttribute("process");t&&await this.wf.goto("start",t),this.isInitialized=!0}attributeChangedCallback(t,e,s){this.isInitialized&&e!=s&&"process"==t&&s&&this.wf.goto("start",s)}createContainer(){const t=this.useShadow()?this.attachShadow({mode:"open"}):this,e=this.getAttribute("styleUrl");null!=e&&"null"!=e&&(t.innerHTML=`<style>@import '${e}'</style>`),this.container=document.createElement("div"),t.appendChild(this.container)}useShadow(){return!("false"===this.getAttribute("shadow"))}}customElements.define("sirius-wf",p)}));