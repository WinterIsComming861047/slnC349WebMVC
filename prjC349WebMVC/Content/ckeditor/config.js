/**
 * @license Copyright (c) 2003-2022, CKSource Holding sp. z o.o. All rights reserved.
 * For licensing, see https://ckeditor.com/legal/ckeditor-oss-license
 */

CKEDITOR.editorConfig = function (config) {
	config.enterMode = CKEDITOR.ENTER_BR;
	config.shiftenterMode = CKEDITOR.ENTER_BR;
	config.htmlEncodeOutput = true;
	config.toolbarGroups = [
		{ name: 'document', groups: ['mode', 'document', 'doctools'] },
		{ name: 'clipboard', groups: ['clipboard', 'undo'] },
		{ name: 'editing', groups: ['find', 'selection', 'spellchecker', 'editing'] },
		{ name: 'forms', groups: ['forms'] },
		'/',
		{ name: 'basicstyles', groups: ['basicstyles', 'cleanup'] },
		{ name: 'paragraph', groups: ['list', 'indent', 'blocks', 'align', 'bidi', 'paragraph'] },
		{ name: 'links', groups: ['links'] },
		{ name: 'insert', groups: ['insert'] },
		'/',
		{ name: 'styles', groups: ['styles'] },
		{ name: 'colors', groups: ['colors'] },
		{ name: 'tools', groups: ['tools'] },
		{ name: 'about', groups: ['about'] },
		{ name: 'others', groups: ['others'] }
	];
	config.height = 500;
	config.width = 1000;
	config.resize_maxWidth = 1920;
	config.resize_enabled = true;
	//config.startupMode='source';
	config.removeButtons = 'About,Iframe,Save,NewPage,Scayt,Blockquote,Language,ExportPdf,ShowBlocks,BidiLtr,BidiRtl,Anchor,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,CreateDiv';

};
