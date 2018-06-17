import { Link } from 'react-router-dom';
import React from 'react';
import agent from '../../agent';
import { connect } from 'react-redux';
import { DELETE_COMPONENT } from '../../constants/actionTypes';

const mapDispatchToProps = dispatch => ({
  onClickDelete: payload =>
    dispatch({ type: DELETE_COMPONENT, payload })
});

const ComponentActions = props => {
  const component = props.component;
  const del = () => {
    props.onClickDelete(agent.Components.del(component.slug))
  };
  if (props.canModify) {
    return (
      <span>

        <Link
          to={`/editor-component/${component.slug}`}
          className="btn btn-outline-secondary btn-sm">
          <i className="ion-edit"></i> Editar componente
        </Link>
        &nbsp; &nbsp;
        <button className="btn btn-danger btn-sm pull-xs-right" onClick={del}>
          <i className="ion-trash-a"></i> Deletar componente
        </button>

      </span>
    );
  }

  return (
    <span>
    </span>
  );
};

export default connect(() => ({}), mapDispatchToProps)(ComponentActions);
