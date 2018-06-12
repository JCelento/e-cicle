import ProjectMeta from './ProjectMeta';
import CommentContainer from './CommentContainer';
import React from 'react';
import agent from '../../agent';
import { connect } from 'react-redux';
import marked from 'marked';
import { PROJECT_PAGE_LOADED, PROJECT_PAGE_UNLOADED } from '../../constants/actionTypes';

const mapStateToProps = state => ({
  ...state.project,
  currentUser: state.common.currentUser
});

const mapDispatchToProps = dispatch => ({
  onLoad: payload =>
    dispatch({ type: PROJECT_PAGE_LOADED, payload }),
  onUnload: () =>
    dispatch({ type: PROJECT_PAGE_UNLOADED })
});

class Project extends React.Component {
  componentWillMount() {
    this.props.onLoad(Promise.all([
      agent.Projects.get(this.props.match.params.id),
      agent.Comments.forProject(this.props.match.params.id)
    ]));
  }

  componentWillUnmount() {
    this.props.onUnload();
  }

  render() {
    if (!this.props.project) {
      return null;
    }

    const markup = { __html: marked(this.props.project.body, { sanitize: true }) };
    const canModify = this.props.currentUser &&
      this.props.currentUser.username === this.props.project.author.username;
    return (
      
      <div className="article-page">

        <div className="banner">
          <div className="container">

            <h1>{this.props.project.title}</h1>
            <ProjectMeta
              project={this.props.project}
              canModify={canModify} />
          </div>
        </div>

        <div className="container page">

          <div className="row article-content">
            <div className="col-xs-12">

              <div classname="card-img">
                <img src={this.props.project.projectImage}/>
              </div>
              <br/>
              <div dangerouslySetInnerHTML={markup}></div>

              <br/>
              <br/>

              <ul className="tag-list">
                Tags:<br/>{
                  this.props.project.tagList.map(tag => {
                    return (
                      <li
                        className="tag-default tag-pill tag-outline"
                        key={tag}>
                        {tag}
                      </li>
                    );
                  })
                }
              </ul>
              <br/>
               <ul className="tag-list">
                Componentes eletrônicos re-utilizados: <br/> {
                  this.props.project.componentList.map(component => {
                    return (
                      <li
                        className="tag-default tag-pill tag-primary"
                        key={component}>
                        {component}
                      </li>
                    );
                  })
                }
              </ul>
            </div>
          </div>
          <hr />
          <div className="article-actions">
          </div>

          <div className="row">
            <CommentContainer
              comments={this.props.comments || []}
              errors={this.props.commentErrors}
              slug={this.props.match.params.id}
              currentUser={this.props.currentUser} />
          </div>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Project);
