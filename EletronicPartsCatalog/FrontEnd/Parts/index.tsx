import * as ReactDOM from 'react-dom';
import * as React from 'react';

import '../Shared/Styles/helpers.scss';

import PartsListContainer from './Components/PartsListContainer/PartsListContainer';

ReactDOM.render(
	<PartsListContainer/ >, 
		document.getElementById('react-root')
	);

declare var module: any;
if (module.hot){
	module.hot.accept();
}