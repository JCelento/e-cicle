import {
  APP_LOAD,
  REDIRECT,
  LOGOUT,
  PROJECT_SUBMITTED,
  COMPONENT_SUBMITTED,
  DELETE_COMPONENT,
  SETTINGS_SAVED,
  LOGIN,
  REGISTER,
  DELETE_PROJECT,
  PROJECT_PAGE_UNLOADED,
  EDITOR_PAGE_UNLOADED,
  HOME_PAGE_UNLOADED,
  PROFILE_PAGE_UNLOADED,
  PROFILE_FAVORITES_PAGE_UNLOADED,
  SETTINGS_PAGE_UNLOADED,
  LOGIN_PAGE_UNLOADED,
  REGISTER_PAGE_UNLOADED,
  UPDATE_FIELD_SEARCH,
  SEARCH
} from '../constants/actionTypes';

const defaultState = {
  appName: 'e-Cicle',
  token: null,
  viewChangeCounter: 0
};

export default (state = defaultState, action) => {
  switch (action.type) {
    case APP_LOAD:
      return {
        ...state,
        token: action.token || null,
        appLoaded: true,
        currentUser: action.payload ? action.payload.user : null
      };
    case REDIRECT:
      return { ...state, redirectTo: null };
    case LOGOUT:
      return { ...state, redirectTo: '/', token: null, currentUser: null };
    case PROJECT_SUBMITTED:
      var redirectUrlProj =  `/editor/`;
    
      if(action.payload.project != null)
        redirectUrlProj = `/project/${action.payload.project.slug}`;
      
      return { ...state, redirectTo: redirectUrlProj };
    case COMPONENT_SUBMITTED:
      var redirectUrlComp=  `/editor-component/`;

      if(action.payload.component != null)
        redirectUrlComp = `/component/${action.payload.component.slug}`;

      return { ...state, redirectTo: redirectUrlComp };
    case SETTINGS_SAVED:
      return {
        ...state,
        redirectTo: action.error ? null : '/',
        currentUser: action.error ? null : action.payload.user
      };
    case LOGIN:
    case REGISTER:
      return {
        ...state,
        redirectTo: action.error ? null : '/',
        token: action.error ? null : action.payload.user.token,
        currentUser: action.error ? null : action.payload.user
      };
    case DELETE_PROJECT:
      return { ...state, redirectTo: '/' };
    case DELETE_COMPONENT:
      return { ...state, redirectTo: '/' };
    case UPDATE_FIELD_SEARCH:
      return { ...state, [action.key]: action.value };
    case SEARCH:
      return {...state, projects: action.payload[0].projects};  
    case PROJECT_PAGE_UNLOADED:
    case EDITOR_PAGE_UNLOADED:
    case HOME_PAGE_UNLOADED:
    case PROFILE_PAGE_UNLOADED:
    case PROFILE_FAVORITES_PAGE_UNLOADED:
    case SETTINGS_PAGE_UNLOADED:
    case LOGIN_PAGE_UNLOADED:
    case REGISTER_PAGE_UNLOADED:
      return { ...state, viewChangeCounter: state.viewChangeCounter + 1 };
    default:
      return state;
  }
};
