import{_ as e}from"./_tslib-6e8ca86b.js";import{b as t}from"./browser-d96520d8.js";import{E as s}from"./eventhelper-8570b930.js";import{m as i}from"./constants-86572358.js";import{F as n,B as o,a as r}from"./text-editor-2c313fd3.js";import{D as c,a as l}from"./input-158e4fdf.js";import{C as a}from"./custom-events-helper-e7f279d3.js";import{n as p}from"./property-4ec0b52d.js";import"./single-slot-element-base-9e7a4622.js";import"./data-qa-utils-8be7c726.js";import"./const-90026e45.js";import"./dx-ui-element-822e6c84.js";import"./lit-element-base-32a69cc0.js";import"./dx-license-dd341102.js";import"./lit-element-462e7ad3.js";import"./logicaltreehelper-7b19cc30.js";import"./layouthelper-0c7c89da.js";import"./point-e4ec110e.js";import"./constants-791d6f9b.js";import"./css-classes-cee8476c.js";import"./devices-afeafb19.js";import"./dom-da46d023.js";import"./common-f853e871.js";import"./focus-utils-405a64a8.js";import"./key-f9cbed1b.js";import"./touch-4bff3f51.js";import"./disposable-d2c2d283.js";import"./keyboard-navigation-strategy-940ff3b3.js";import"./focushelper-cb993bae.js";import"./dom-utils-c35907a1.js";import"./custom-element-267f9a21.js";class d{constructor(e){this.deltaY=e}}var u,h;!function(e){e[e.forward=0]="forward",e[e.backward=1]="backward"}(u||(u={}));class m{constructor(e,t,s,i){this.direction=u.backward,this.selectionStart=e,this.selectionEnd=t,this.selectAll=i,this.direction=s}}class f extends Event{constructor(){super(f.eventName,{bubbles:!0,composed:!0,cancelable:!0})}}f.eventName=i+".applyValue";class y extends CustomEvent{constructor(e){super(y.eventName,{detail:new n(e,!1),bubbles:!0,composed:!0,cancelable:!0})}}y.eventName=i+".autofill";class b extends CustomEvent{constructor(e){super(b.eventName,{detail:new d(e),bubbles:!0,composed:!0,cancelable:!0})}}b.eventName=i+".wheel";class w extends CustomEvent{constructor(e,t,s,i){super(w.eventName,{detail:new m(e,t,s,i),bubbles:!0,composed:!0,cancelable:!0})}}w.eventName=i+".selectionchange";class v extends CustomEvent{constructor(e){super(v.eventName,{detail:e,bubbles:!0,composed:!0,cancelable:!0})}}v.eventName=i+".compositionEnd",a.register(w.eventName,(e=>e.detail)),a.register(y.eventName,(e=>e.detail)),a.register(f.eventName,(e=>e.detail)),a.register(b.eventName,(e=>e.detail)),a.register(v.eventName,(e=>({Text:e.detail.data}))),function(e){e.syncSelectionStart="sync-selection-start",e.syncSelectionEnd="sync-selection-end",e.isMaskDefined="is-mask-defined"}(h||(h={}));const S={...h,...c};class E extends l{constructor(){super(...arguments),this.wheelTimerId=-1,this._shouldApplySelectionOnFocus=!0,this.compositionProcessing=!1,this.compositionEndHandler=this.handleCompositionEnd.bind(this),this.pointerUpHandler=this.handlePointerUp.bind(this),this.clickHandler=this.handleClick.bind(this),this.wheelHandler=this.handleWheel.bind(this),this.syncSelectionStart=null,this.syncSelectionEnd=null,this.isMaskDefined=!1}disconnectedCallback(){super.disconnectedCallback()}get shouldProcessFocusOut(){return!0}get shouldProcessWheel(){return this.isMaskDefined}get shouldProcessFocusIn(){return!0}get shouldApplySelectionOnFocus(){return this._shouldApplySelectionOnFocus}set shouldApplySelectionOnFocus(e){this._shouldApplySelectionOnFocus=e}get inputSelectionDirection(){var e;return"backward"===(null===(e=this.inputElement)||void 0===e?void 0:e.selectionDirection)?u.backward:u.forward}handlePointerUp(e){this.allowInput&&this.isMaskDefined&&this.processPointerUp(e)}handleCompositionEnd(e){this.dispatchEvent(new v(e)),this.inputElement.removeEventListener("compositionend",this.compositionEndHandler),this.compositionProcessing=!1}handleClick(e){this.applyInputSelection()}handleWheel(e){if(this.shouldProcessWheel&&(e.preventDefault(),this.processWheel(e),this.bindValueMode===o.OnLostFocus)){-1!==this.wheelTimerId&&clearTimeout(this.wheelTimerId);this.wheelTimerId=window.setTimeout((()=>{this.raiseApplyValue()}).bind(this),500)}}processWheel(e){this.dispatchEvent(new b(e.deltaY))}applyTextPropertyCore(){super.applyTextPropertyCore(),this.applyMaskManagerSelection()}applyMaskManagerSelection(){!this.isMaskDefined||!this.focused&&(t.Browser.MacOSPlatform||t.Browser.MacOSMobilePlatform)||this.inputSelectionStart===this.syncSelectionStart&&this.inputSelectionEnd===this.syncSelectionEnd||this.selectInputText(this.syncSelectionStart,this.syncSelectionEnd)}get shouldProcessFieldTextVersion(){return!this.isMaskDefined&&super.shouldProcessFieldTextVersion}get shouldRaiseFieldTextEvents(){return!this.isMaskDefined}processFocusIn(){var e;super.processFocusIn(),this.allowInput&&this.isMaskDefined&&this.shouldApplySelectionOnFocus&&this.applyInputSelection(),this.addEventListener("wheel",this.wheelHandler,{capture:!0,passive:!1}),t.Browser.WebKitTouchUI&&(null===(e=this.inputElement)||void 0===e||e.addEventListener("click",this.clickHandler))}processFocusOut(e){var s;super.processFocusOut(e),this.removeEventListener("wheel",this.wheelHandler,{capture:!0}),t.Browser.WebKitTouchUI&&(null===(s=this.inputElement)||void 0===s||s.removeEventListener("click",this.clickHandler))}processKeyDown(e){return super.processKeyDown(e),!!this.shouldProcessKeyDown(e)&&(s.markHandled(e),this.raiseKeyDown(e),!0)}processPointerDown(e){return super.processPointerDown(e),this.isMaskDefined&&!this.focused&&(this.shouldApplySelectionOnFocus=!1),s.containsInComposedPath(e,(e=>e===this.inputElement))&&document.addEventListener(E.pointerUpEventType,this.pointerUpHandler),!0}processKeyUp(e){if(!this.inputElement||!s.containsInComposedPath(e,(e=>e===this.inputElement)))return!1;const t=this.querySelector("input:-webkit-autofill");return this.isMaskDefined&&t&&t.value&&!this.isOpenDropDownShortcut(e)&&(this.dispatchEvent(new y(t.value)),s.markHandled(e)),this.shouldProcessKeyUp(e)&&(this.bindValueMode===o.OnLostFocus?this.raiseApplyValue():this.applyDefferedValue()),!0}applyDefferedValue(){this.bindValueMode===o.OnDelayedInput&&this.inputDelayDeferredAction.execute((()=>this.raiseApplyValue()))}raiseApplyValue(){this.dispatchEvent(new f)}processBeforeInput(e){return!!super.processBeforeInput(e)||!!this.isMaskDefined&&(e.isComposing&&!t.Browser.AndroidMobilePlatform?(this.compositionProcessing||(this.inputElement.addEventListener("compositionend",this.compositionEndHandler),this.compositionProcessing=!0),!0):(s.markHandled(e),this.shouldProcessBeforeInput(e)&&(this.applyInputSelection(),this.dispatchEvent(new r(e)),this.applyDefferedValue()),!0))}updated(e){super.updated(e)}shouldProcessKeyUp(e){if(!this.isMaskDefined)return!1;switch(e.key){case"ArrowUp":case"ArrowDown":case"Enter":return!0;case"z":return e.ctrlKey}return!1}shouldProcessKeyDown(e){if(!this.isMaskDefined)return!1;switch(e.code){case"ArrowLeft":case"ArrowRight":case"ArrowUp":case"ArrowDown":case"Home":case"Delete":case"End":return!0;case"KeyA":case"KeyZ":return e.ctrlKey}return!1}shouldProcessBeforeInput(e){switch(e.inputType){case"insertText":case"insertReplacementText":case"insertFromPaste":return null!==e.data&&e.data.length>0;case"deleteContentBackward":case"deleteContentForward":case"deleteByCut":return!0;case"insertCompositionText":return t.Browser.AndroidMobilePlatform}return!1}isOpenDropDownShortcut(e){return"ArrowDown"===e.code&&e.altKey}selectAll(){super.selectAll(),this.focused&&this.applyInputSelection()}processPointerUp(e){return this.applyInputSelection(),this.shouldApplySelectionOnFocus&&(this.shouldApplySelectionOnFocus=!0),document.removeEventListener(E.pointerUpEventType,this.pointerUpHandler),!0}applyInputSelection(){if(this.isMaskDefined&&(this.syncSelectionStart!==this.inputSelectionStart||this.syncSelectionEnd!==this.inputSelectionEnd)){const e=this.fieldElementValue.length===Math.abs(this.inputSelectionEnd-this.inputSelectionStart);this.raiseMaskSelectionChanged(this.inputSelectionStart,this.inputSelectionEnd,this.inputSelectionDirection,e)}}raiseMaskSelectionChanged(e,t,s,i){this.dispatchEvent(new w(e,t,s,i))}}E.pointerUpEventType=t.Browser.WebKitTouchUI?"touchend":"pointerup",e([p({type:Number,attribute:S.syncSelectionStart})],E.prototype,"syncSelectionStart",void 0),e([p({type:Number,attribute:S.syncSelectionEnd})],E.prototype,"syncSelectionEnd",void 0),e([p({type:Boolean,attribute:S.isMaskDefined})],E.prototype,"isMaskDefined",void 0),customElements.define(i,E);const k={loadModule:function(){}};export{E as DxMaskedInputEditor,S as DxMaskedInputEditorAttributes,k as default};