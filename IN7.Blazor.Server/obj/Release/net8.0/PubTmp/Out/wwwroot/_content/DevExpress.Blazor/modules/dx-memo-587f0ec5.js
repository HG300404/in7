import{_ as e}from"./_tslib-6e8ca86b.js";import{b as t}from"./text-editor-2c313fd3.js";import{h as s}from"./constants-86572358.js";import{C as i}from"./css-classes-cee8476c.js";import{e as o}from"./custom-element-267f9a21.js";import"./single-slot-element-base-9e7a4622.js";import"./data-qa-utils-8be7c726.js";import"./const-90026e45.js";import"./dx-ui-element-822e6c84.js";import"./lit-element-base-32a69cc0.js";import"./dx-license-dd341102.js";import"./lit-element-462e7ad3.js";import"./logicaltreehelper-7b19cc30.js";import"./layouthelper-0c7c89da.js";import"./point-e4ec110e.js";import"./constants-791d6f9b.js";import"./property-4ec0b52d.js";import"./custom-events-helper-e7f279d3.js";import"./eventhelper-8570b930.js";import"./devices-afeafb19.js";import"./dom-da46d023.js";import"./browser-d96520d8.js";import"./common-f853e871.js";import"./focus-utils-405a64a8.js";import"./key-f9cbed1b.js";import"./touch-4bff3f51.js";import"./disposable-d2c2d283.js";class r{}r.Memo=i.Prefix+"-memo-edit";let n=class extends t{constructor(){super(),this.textAreaObserver=new MutationObserver(this.textAreaSizeChanged.bind(this))}connectedOrContentChanged(){super.connectedOrContentChanged();const e=this.getFieldElement();e&&this.textAreaObserver.observe(e,{attributes:!0})}disconnectedCallback(){this.textAreaObserver.disconnect(),super.disconnectedCallback()}textAreaSizeChanged(e,t){const s=e[0].target,i=parseInt(s.style.width);if(!isNaN(i)){const e=this.offsetWidth-this.clientWidth+i;this.offsetWidth!==e&&(this.style.width=e+"px")}const o=parseInt(s.style.height);if(!isNaN(o)){const e=parseInt(getComputedStyle(s).minHeight),t=this.offsetHeight-this.clientHeight+(o>=e?o:e);this.offsetHeight!==t&&(this.style.height=t+"px")}}get shouldForceInputOnEnter(){return!1}getFieldElement(){return this.querySelector("textarea")}};n=e([o(s)],n);const c={loadModule:function(){}};export{n as DxMemoEditor,r as MemoCssClasses,c as default};