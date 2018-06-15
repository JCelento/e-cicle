import ComponentMeta from './ComponentMeta';
import React from 'react';
import agent from '../../agent';
import { connect } from 'react-redux';
import { Link } from 'react-router-dom';
import { COMPONENT_PAGE_LOADED, COMPONENT_PAGE_UNLOADED } from '../../constants/actionTypes';

const mapStateToProps = state => ({
  ...state.component,
  currentUser: state.common.currentUser
});

const mapDispatchToProps = dispatch => ({
  onLoad: payload =>
    dispatch({ type: COMPONENT_PAGE_LOADED, payload }),
  onUnload: () =>
    dispatch({ type: COMPONENT_PAGE_UNLOADED })
});

class Component extends React.Component {
  componentWillMount() {
    this.props.onLoad(Promise.all([
      agent.Components.get(this.props.match.params.slug)
    ]));
  }

  componentWillUnmount() {
    this.props.onUnload();
  }

  render() {
    if (!this.props.component) {
      return null;
    }

    const canModify = this.props.currentUser;
    return (
      
      <div className="component-page">

        <div className="banner">
          <div className="container">
          <div className="col-xs-12">
            <h1>{this.props.component.componentId}</h1>
            <ComponentMeta
              component={this.props.component}
              canModify={canModify} />
      </div>
          </div>
        </div>

        <div className="container page">

        <Link to={`/@${this.props.component.slug}`}>
            <img src={this.props.component.componentImage} alt={this.props.component.slug} />
            </Link>
          <br/>
          <br/>
          <div className="row article-content">
            <div className="col-xs-12">
            Descrição do componente:
            <br/>
            <div>{this.props.component.description}</div>
            <br/>
            Esse componente pode ser encontrado nos seguintes tipos de lixo eletrônico:
            <br/>
              <br/>
              <ul className="tag-list">
                {
                  this.props.component.whereToFindItList.map(whereToFindIt => {
                    return (
                      <li
                        className="tag-default tag-pill tag-info"
                        key={whereToFindIt}>
                        {whereToFindIt}
                      </li>
                    );
                  })
                }
              </ul>

            </div>
          </div>

          <hr />

          <div className="component-actions">
          </div>
        </div>
     </div>
    );

    if(this.props.currentUser == null){
      return(
        <div className="col-xs-12 offset-md-5">
        <p>
          <Link to="/login">Faça Login</Link>
          &nbsp;ou&nbsp;
          <Link to="/register">Cadastre-se</Link>
        </p>
        </div> 
      );
    }
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(Component);
