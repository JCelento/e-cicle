import React from 'react';
import agent from '../../agent';

const Components = props => {
  const components = props.components;
  if (components) {
    return (
      <div className="tag-list">
        {
          components.map(component => {
            const handleClick = ev => {
              ev.preventDefault();
              props.onClickComponent(component, page => agent.Projects.byComponent(component.componentId, page), agent.Projects.byComponent(component.componentId));
            };

            return (
              <a
                href=""
                className="tag-default tag-pill tag-primary"
                key={component.componentId}
                onClick={handleClick}>
                {component.componentId}
              </a>
            );
          })
        }
      </div>
    );
  } else {
    return (
      <div>Carregando Componentes...</div>
    );
  }
};

export default Components;
