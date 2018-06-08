import ProjectPreview from './ProjectPreview';
import ListPagination from './ListPagination';
import React from 'react';

const ProjectList = props => {
  if (!props.projects) {
    return (
      <div className="project-preview">Carregando...</div>
    );
  }

  if (props.projects.length === 0) {
    return (
      <div className="project-preview">
        Não encontramos nenhum projeto para mostrar :(
      </div>
    );
  }

  return (
    <div>
      {
        props.projects.map(project => {
          return (
            <ProjectPreview project={project} key={project.slug} />
          );
        })
      }

      <ListPagination
        pager={props.pager}
        projectsCount={props.projectsCount}
        currentPage={props.currentPage} />
    </div>
  );
};

export default ProjectList;
