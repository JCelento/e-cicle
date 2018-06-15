import {
  EDITOR_COMPONENT_PAGE_LOADED,
  EDITOR_COMPONENT_PAGE_UNLOADED,
  COMPONENT_SUBMITTED,
  ASYNC_START,
  ADD_WHERE_TO_FIND,
  REMOVE_WHERE_TO_FIND,
  UPDATE_FIELD_COMPONENT_EDITOR
} from '../constants/actionTypes';

export default (state = {}, action) => {
  switch (action.type) {
    case EDITOR_COMPONENT_PAGE_LOADED:
      return {
        ...state,
        componentSlug: action.payload ? action.payload.component.slug : '',
        name: action.payload ? action.payload.component.componentId : '',
        description: action.payload ? action.payload.component.description : '',
        componentImage: action.payload ? action.payload.component.componentImage : '',
        whereToFindItInput: '',
        whereToFindItList: action.payload ? action.payload.component.whereToFindItList : []
      };
    case EDITOR_COMPONENT_PAGE_UNLOADED:
      return {};
    case COMPONENT_SUBMITTED:
      return {
        ...state,
        inProgress: null,
        errors: action.error ? action.payload.errors : null
      };
    case ASYNC_START:
      if (action.subtype === COMPONENT_SUBMITTED) {
        return { ...state, inProgress: true };
      }
      break;
      case ADD_WHERE_TO_FIND:
      return {
        ...state,
        whereToFindItList: state.whereToFindItList.concat([state.whereToFindItInput]),
        whereToFindItInput: ''
      };
    case REMOVE_WHERE_TO_FIND:
      return {
        ...state,
        whereToFindItList: state.whereToFindItList.filter(whereToFindIt => whereToFindIt !== action.whereToFindIt)
      };

    case UPDATE_FIELD_COMPONENT_EDITOR:
      return { ...state, [action.key]: action.value };
    default:
      return state;
  }

  return state;
};
