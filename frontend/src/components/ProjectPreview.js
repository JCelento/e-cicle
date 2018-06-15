import React from 'react';
import { Link } from 'react-router-dom';
import agent from '../agent';
import { connect } from 'react-redux';
import { PROJECT_FAVORITED, PROJECT_UNFAVORITED } from '../constants/actionTypes';
import { formatDate } from '../util.js';
import { slugfy } from '../util.js';

const FAVORITED_CLASS = 'btn btn-sm btn-primary';
const NOT_FAVORITED_CLASS = 'btn btn-sm btn-outline-primary';

const mapDispatchToProps = dispatch => ({
  favorite: slug => dispatch({
    type: PROJECT_FAVORITED,
    payload: agent.Projects.favorite(slug)
  }),
  unfavorite: slug => dispatch({
    type: PROJECT_UNFAVORITED,
    payload: agent.Projects.unfavorite(slug)
  })
});

const ProjectPreview = props => {
  const project = props.project;
  const favoriteButtonClass = project.favorited ?
    FAVORITED_CLASS :
    NOT_FAVORITED_CLASS;

  const handleClick = ev => {
    ev.preventDefault();
    if (project.favorited) {
      props.unfavorite(project.slug);
    } else {
      props.favorite(project.slug);
    }
  };

  return (
    <div className="article-preview">
      <div className="article-meta">
        <Link to={`/@${project.author.username}`}>
          <img src={project.author.image} alt={project.author.username}  width="50"/>
        </Link>

        <div className="info">
          <Link className="author" to={`/@${project.author.username}`}>
            {project.author.username+" "} 
          </Link>
          <span className="date">
            {formatDate(new Date(project.createdAt))}
          </span>
        </div>

        <div className="pull-xs-right">
          <button className={favoriteButtonClass} onClick={handleClick}>
            <i className="ion-heart"></i> {project.favoritesCount}
          </button>
        </div>
      </div>

      <Link to={`/project/${project.slug}`} className="preview-link"/>
        <h1>{project.title}</h1>
        <div className="project-meta">
        <Link to={`project/${project.slug}`}>
          <img src={project.projectImage} alt={project.slug} width="500px"/>
        </Link>
        </div>
        <p>{project.description}</p>
        <span>Continue lendo... </span>
        <ul className="tag-list">
          {
            project.tagList.map(tag => {
              return (
                <li className="tag-default tag-pill tag-outline" key={tag}>
                  {tag}
                </li>
              )
            })
          }
        </ul>
        <ul className="tag-list">
          {
            project.componentList.map(component => {
              return (
                <Link to={`/component/${slugfy(component)}`} className="preview-link" key={slugfy(component)}>
                <li className="tag-default tag-pill tag-primary" key={component}>
                  {component}
                </li>
                </Link>
              )
            })
          }
        </ul>
    </div>
  );
}

export default connect(() => ({}), mapDispatchToProps)(ProjectPreview);
