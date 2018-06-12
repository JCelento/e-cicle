import project from './reducers/project';
import projectList from './reducers/projectList';
import auth from './reducers/auth';
import { combineReducers } from 'redux';
import common from './reducers/common';
import editor from './reducers/editor';
import editorComponent from './reducers/editorComponent'
import home from './reducers/home';
import component from './reducers/component'
import profile from './reducers/profile';
import settings from './reducers/settings';
import { routerReducer } from 'react-router-redux';

export default combineReducers({
  project,
  projectList,
  auth,
  common,
  editor,
  editorComponent,
  home,
  profile,
  component,
  settings,
  router: routerReducer
});
