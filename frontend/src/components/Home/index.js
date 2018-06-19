import Banner from './Banner';
import Description from './Description';
import MainView from './MainView';
import React from 'react';
import Tags from './Tags';
import Components from './Components';
import SearchBar from '../SearchBar';
import agent from '../../agent';
import { connect } from 'react-redux';
import {
  HOME_PAGE_LOADED,
  HOME_PAGE_UNLOADED,
  APPLY_TAG_FILTER,
  APPLY_SEARCH_FILTER
} from '../../constants/actionTypes';

const Promise = global.Promise;

const mapStateToProps = state => ({
  ...state.home,
  appName: state.common.appName,
  token: state.common.token
});

const mapDispatchToProps = dispatch => ({
  onClickTag: (tag, pager, payload) =>
    dispatch({ type: APPLY_TAG_FILTER, tag, pager, payload }),
  onClickComponent: (component, pager, payload) =>
    dispatch({ type: APPLY_SEARCH_FILTER, component, pager, payload }),
  onLoad: (tab, pager, payload) =>
    dispatch({ type: HOME_PAGE_LOADED, tab, pager, payload }),
  onUnload: () =>
    dispatch({  type: HOME_PAGE_UNLOADED })
});

class Home extends React.Component {
  componentWillMount() {
    const tab = this.props.token ? 'feed' : 'all';
    const projectsPromise = this.props.token ?
      agent.Projects.feed :
      agent.Projects.all;

    this.props.onLoad(tab, projectsPromise, Promise.all([agent.Tags.getAll(), agent.Components.all(), projectsPromise()]));
  }

  componentWillUnmount() {
    this.props.onUnload();
  }

  render() {
    return (
      <div className="home-page">

        <Banner token={this.props.token} appName={this.props.appName} />

        <Description token={this.props.token}/>

        <div className="container page">
          <div className="row">
            <MainView />

            <div className="col-md-3">
              <div className="sidebar">
              
              <SearchBar />

                <p>Tags Populares <i className="ion-ios-pricetag-outline"></i></p>

                <Tags
                  tags={this.props.tags}
                  onClickTag={this.props.onClickTag} />

                  
                <p>Componentes Populares <i className="ion-ios-cog-outline"></i></p>
                
                <Components
                  components={this.props.components}
                  onClickComponent={this.props.onClickComponent} />

              </div>
            </div>
          </div>
        </div>

      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Home);
