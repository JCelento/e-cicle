import { HOME_PAGE_LOADED, HOME_PAGE_UNLOADED } from '../constants/actionTypes';

export default (state = {}, action) => {
  switch (action.type) {
    case HOME_PAGE_LOADED:
      return {
        ...state,
        tags: action.payload[0].tags,
        components: action.payload[1].components
      };
    case HOME_PAGE_UNLOADED:
      return { };
    default:
      return state;
  }
};
