import {
  EDITOR_PAGE_LOADED,
  EDITOR_PAGE_UNLOADED,
  PROJECT_SUBMITTED,
  ASYNC_START,
  ADD_TAG,
  REMOVE_TAG,
  ADD_COMPONENT,
  REMOVE_COMPONENT,
  UPDATE_FIELD_EDITOR
} from '../constants/actionTypes';

export default (state = {}, action) => {
  switch (action.type) {
    case EDITOR_PAGE_LOADED:
      return {
        ...state,
        projectSlug: action.payload ? action.payload.project.slug : '',
        title: action.payload ? action.payload.project.title : '',
        description: action.payload ? action.payload.project.description : '',
        body: action.payload ? action.payload.project.body : '',
        projectImage: action.payload ? action.payload.project.projectImage : '',
        tagInput: '',
        tagList: action.payload ? action.payload.project.tagList : [],
        componentInput: '',
        componentList: action.payload ? action.payload.project.componentList : []
      };
    case EDITOR_PAGE_UNLOADED:
      return {};
    case PROJECT_SUBMITTED:
      return {
        ...state,
        inProgress: null,
        errors: action.error ? action.payload.errors : null
      };
    case ASYNC_START:
      if (action.subtype === PROJECT_SUBMITTED) {
        return { ...state, inProgress: true };
      }
      break;
    case ADD_TAG:
      return {
        ...state,
        tagList: state.tagList.concat([state.tagInput]),
        tagInput: ''
      };
    case REMOVE_TAG:
      return {
        ...state,
        tagList: state.tagList.filter(tag => tag !== action.tag)
      };

      case ADD_COMPONENT:
      return {
        ...state,
        componentList: state.componentList.concat([state.componentInput]),
        componentInput: ''
      };
    case REMOVE_COMPONENT:
      return {
        ...state,
        componentList: state.componentList.filter(component => component !== action.component)
      };
    case UPDATE_FIELD_EDITOR:
      return { ...state, [action.key]: action.value };
    default:
      return state;
  }

  return state;
};
