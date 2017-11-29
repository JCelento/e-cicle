import * as ReactDOM from 'react-dom';
import * as React from 'react';

import '../Shared/Styles/helpers.scss';

import ObjectsListContainer from './Components/ObjectsListContainer/ObjectsListContainer';

ReactDOM.render(
	<ObjectsListContainer/ >, 
		document.getElementById('react-root')
	);

declare var module: any;
if (module.hot){
	module.hot.accept();
}