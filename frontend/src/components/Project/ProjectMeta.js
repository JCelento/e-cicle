import ProjectActions from './ProjectActions';
import { Link } from 'react-router-dom';
import React from 'react';
import { formatDate } from '../../util.js';

const ProjectMeta = props => {
  const project = props.project;
  return (
    <div className="article-meta">
      <Link to={`/@${project.author.username}`}>
        <img src={project.author.image} alt={project.author.username} />
      </Link>

      <div className="info">
        <Link to={`/@${project.author.username}`} className="author">
          {project.author.username + " "}
        </Link>
        <span className="date">
          {formatDate(new Date(project.createdAt))}
        </span>
      </div>

      <ProjectActions canModify={props.canModify} project={project} />
    </div>
  );
};

export default ProjectMeta;
