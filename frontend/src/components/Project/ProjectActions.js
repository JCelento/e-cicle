import { Link } from 'react-router-dom';
import React from 'react';
import agent from '../../agent';
import { connect } from 'react-redux';
import { DELETE_PROJECT } from '../../constants/actionTypes';

const mapDispatchToProps = dispatch => ({
  onClickDelete: payload =>
    dispatch({ type: DELETE_PROJECT, payload })
});

const ProjectActions = props => {
  const project = props.project;
  const del = () => {
    props.onClickDelete(agent.Projects.del(project.slug))
  };
  if (props.canModify) {
    return (
      <span>

        <Link
          to={`/editor/${project.slug}`}
          className="btn btn-outline-secondary btn-sm">
          <i className="ion-edit"></i> Editar projeto
        </Link>
        
        &nbsp; &nbsp;

        <button className="btn btn-outline-danger btn-sm" onClick={del}>
          <i className="ion-trash-a"></i> Deletar Projeto
        </button>

      </span>
    );
  }

  return (
    <span>
    </span>
  );
};

export default connect(() => ({}), mapDispatchToProps)(ProjectActions);
