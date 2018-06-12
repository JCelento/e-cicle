import {
  COMPONENT_PAGE_LOADED,
  COMPONENT_PAGE_UNLOADED
} from '../constants/actionTypes';

export default (state = {}, action) => {
  switch (action.type) {
    case COMPONENT_PAGE_LOADED:
      return {
        ...state,
        component: action.payload[0].component
      };
    case COMPONENT_PAGE_UNLOADED:
      return {};
    default:
      return state;
  }
};
