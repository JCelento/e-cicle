import ProjectActions from './ProjectActions';
import { Link } from 'react-router-dom';
import React from 'react';

const ProjectMeta = props => {
  const project = props.project;
  return (
    <div className="project-meta">
      <Link to={`/@${project.author.username}`}>
        <img src={project.author.image} alt={project.author.username} />
      </Link>

      <div className="info">
        <Link to={`/@${project.author.username}`} className="author">
          {project.author.username}
        </Link>
        <span className="date">
          {new Date(project.createdAt).toDateString()}
        </span>
      </div>

      <ProjectActions canModify={props.canModify} project={project} />
    </div>
  );
};

export default ProjectMeta;
