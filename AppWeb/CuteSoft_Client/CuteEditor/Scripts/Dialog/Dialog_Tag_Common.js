var OxOac7b=["inp_class","inp_width","inp_height","sel_align","sel_textalign","sel_float","inp_forecolor","img_forecolor","inp_backcolor","img_backcolor","inp_tooltip","className","value","width","style","height","align","styleFloat","textAlign","title","backgroundColor","color","","class","onclick"];var inp_class=Window_GetElement(window,OxOac7b[0x0],true);var inp_width=Window_GetElement(window,OxOac7b[0x1],true);var inp_height=Window_GetElement(window,OxOac7b[0x2],true);var sel_align=Window_GetElement(window,OxOac7b[0x3],true);var sel_textalign=Window_GetElement(window,OxOac7b[0x4],true);var sel_float=Window_GetElement(window,OxOac7b[0x5],true);var inp_forecolor=Window_GetElement(window,OxOac7b[0x6],true);var img_forecolor=Window_GetElement(window,OxOac7b[0x7],true);var inp_backcolor=Window_GetElement(window,OxOac7b[0x8],true);var img_backcolor=Window_GetElement(window,OxOac7b[0x9],true);var inp_tooltip=Window_GetElement(window,OxOac7b[0xa],true); UpdateState=function UpdateState_Common(){}  ; SyncToView=function SyncToView_Common(){ inp_class[OxOac7b[0xc]]=element[OxOac7b[0xb]] ; inp_width[OxOac7b[0xc]]=element[OxOac7b[0xe]][OxOac7b[0xd]] ; inp_height[OxOac7b[0xc]]=element[OxOac7b[0xe]][OxOac7b[0xf]] ; sel_align[OxOac7b[0xc]]=element[OxOac7b[0x10]] ; sel_float[OxOac7b[0xc]]=element[OxOac7b[0xe]][OxOac7b[0x11]] ; sel_textalign[OxOac7b[0xc]]=element[OxOac7b[0xe]][OxOac7b[0x12]] ; inp_tooltip[OxOac7b[0xc]]=element[OxOac7b[0x13]] ; inp_forecolor[OxOac7b[0xc]]=revertColor(element[OxOac7b[0xe]].color) ; inp_forecolor[OxOac7b[0xe]][OxOac7b[0x14]]=inp_forecolor[OxOac7b[0xc]] ; img_forecolor[OxOac7b[0xe]][OxOac7b[0x14]]=inp_forecolor[OxOac7b[0xc]] ; inp_backcolor[OxOac7b[0xc]]=revertColor(element[OxOac7b[0xe]].backgroundColor) ; inp_backcolor[OxOac7b[0xe]][OxOac7b[0x14]]=inp_backcolor[OxOac7b[0xc]] ; img_backcolor[OxOac7b[0xe]][OxOac7b[0x14]]=inp_backcolor[OxOac7b[0xc]] ;}  ; SyncTo=function SyncTo_Common(element){ element[OxOac7b[0xb]]=inp_class[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0xd]]=inp_width[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0xf]]=inp_height[OxOac7b[0xc]] ; element[OxOac7b[0x10]]=sel_align[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0x11]]=sel_float[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0x12]]=sel_textalign[OxOac7b[0xc]] ; element[OxOac7b[0x13]]=inp_tooltip[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0x15]]=inp_forecolor[OxOac7b[0xc]] ; element[OxOac7b[0xe]][OxOac7b[0x14]]=inp_backcolor[OxOac7b[0xc]] ;if(element[OxOac7b[0xb]]==OxOac7b[0x16]){ element.removeAttribute(OxOac7b[0xb]) ;} ;if(element[OxOac7b[0xb]]==OxOac7b[0x16]){ element.removeAttribute(OxOac7b[0x17]) ;} ;if(element[OxOac7b[0x13]]==OxOac7b[0x16]){ element.removeAttribute(OxOac7b[0x13]) ;} ;if(element[OxOac7b[0x10]]==OxOac7b[0x16]){ element.removeAttribute(OxOac7b[0x10]) ;} ;}  ;if(!Browser_IsWinIE()){ img_forecolor[OxOac7b[0x18]]=inp_forecolor[OxOac7b[0x18]]=function inp_forecolor_onclick(){ SelectColor(inp_forecolor,img_forecolor) ;}  ; img_backcolor[OxOac7b[0x18]]=inp_backcolor[OxOac7b[0x18]]=function inp_backcolor_onclick(){ SelectColor(inp_backcolor,img_backcolor) ;}  ;} ;