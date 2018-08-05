import ComponentActions from './ComponentActions';
import React from 'react';
import { formatDate } from '../../util.js';

const ComponentMeta = props => {
  const component = props.component;
  return (
    <div className="component-meta">
      <div className="info">
        <span className="date">
          {formatDate(new Date(component.createdAt))}
        </span>
      </div>

      <ComponentActions canModify={props.canModify && props.canModify.username == component.author.username} component={component} />
    </div>
  );
};

export default ComponentMeta;
