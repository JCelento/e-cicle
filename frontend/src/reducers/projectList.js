import {
  PROJECT_FAVORITED,
  PROJECT_UNFAVORITED,
  SET_PAGE,
  APPLY_TAG_FILTER,
  APPLY_SEARCH_FILTER,
  APPLY_COMPONENT_FILTER,
  HOME_PAGE_LOADED,
  HOME_PAGE_UNLOADED,
  CHANGE_TAB,
  PROFILE_PAGE_LOADED,
  PROFILE_PAGE_UNLOADED,
  PROFILE_FAVORITES_PAGE_LOADED,
  PROFILE_FAVORITES_PAGE_UNLOADED
} from '../constants/actionTypes';

export default (state = {}, action) => {
  switch (action.type) {
    case PROJECT_FAVORITED:
    case PROJECT_UNFAVORITED:
      return {
        ...state,
        projects: state.projects.map(project => {
          if (project.slug === action.payload.project.slug) {
            return {
              ...project,
              favorited: action.payload.project.favorited,
              favoritesCount: action.payload.project.favoritesCount
            };
          }
          return project;
        })
      };
    case SET_PAGE:
      return {
        ...state,
        projects: action.payload.projects,
        projectsCount: action.payload.projectsCount,
        currentPage: action.page
      };
    case APPLY_TAG_FILTER:
      return {
        ...state,
        pager: action.pager,
        projects: action.payload.projects,
        projectsCount: action.payload.projectsCount,
        tab: null,
        tag: action.tag,
        currentPage: 0
      };
    case APPLY_SEARCH_FILTER:
      return {
        ...state,
        pager: action.pager,
        projects: action.payload.projects,
        projectsCount: action.payload.projectsCount,
        tab: null,
        search: action.search,
        currentPage: 0
      };
    case APPLY_COMPONENT_FILTER:
      return {
        ...state,
        pager: action.pager,
        projects: action.payload.projects,
        projectsCount: action.payload.projectsCount,
        tab: null,
        component: action.component.componentId,
        currentPage: 0
      };
    case HOME_PAGE_LOADED:
      return {
        ...state,
        pager: action.pager,
        tags: action.payload[0].tags,
        components: action.payload[1].components,
        projects: action.payload[2].projects,
        projectsCount: action.payload[2].projectsCount,
        currentPage: 0,
        tab: action.tab
      };
    case HOME_PAGE_UNLOADED:
    return { ...state, viewChangeCounter: state.viewChangeCounter + 1 };
    case CHANGE_TAB:
      return {
        ...state,
        pager: action.pager,
        projects: action.payload.projects,
        projectsCount: action.payload.projectsCount,
        tab: action.tab,
        currentPage: 0,
        tag: null
      };
    case PROFILE_PAGE_LOADED:
    case PROFILE_FAVORITES_PAGE_LOADED:
      return {
        ...state,
        pager: action.pager,
        projects: action.payload[1].projects,
        projectsCount: action.payload[1].projectsCount,
        currentPage: 0
      };
    case PROFILE_PAGE_UNLOADED:
    case PROFILE_FAVORITES_PAGE_UNLOADED:
      return {};
    default:
      return state;
  }
};
