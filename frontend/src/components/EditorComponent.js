import ListErrors from './ListErrors';
import React from 'react';
import agent from '../agent';
import { connect } from 'react-redux';
import { Redirect } from 'react-router';
import {
  ADD_WHERE_TO_FIND,
  EDITOR_COMPONENT_PAGE_LOADED,
  REMOVE_WHERE_TO_FIND,
  COMPONENT_SUBMITTED,
  EDITOR_COMPONENT_PAGE_UNLOADED,
  UPDATE_FIELD_EDITOR
} from '../constants/actionTypes';

const mapStateToProps = state => ({
  ...state.editorComponent
});

const mapDispatchToProps = dispatch => ({
  onAddwhereToFindIt: () =>
    dispatch({ type: ADD_WHERE_TO_FIND }),
  onLoad: payload =>
    dispatch({ type: EDITOR_COMPONENT_PAGE_LOADED, payload }),
  onRemovewhereToFindIt: whereToFindIt =>
    dispatch({ type: REMOVE_WHERE_TO_FIND, whereToFindIt }),
  onSubmit: payload =>
    dispatch({ type: COMPONENT_SUBMITTED, payload }),
  onUnload: payload =>
    dispatch({ type: EDITOR_COMPONENT_PAGE_UNLOADED }),
  onUpdateField: (key, value) =>
    dispatch({ type: UPDATE_FIELD_EDITOR, key, value })
});

class EditorComponent extends React.Component {

  state = {
    redirectToNewPage: false
  }

  constructor() {
    super();

    const updateFieldEvent =
      key => ev => this.props.onUpdateField(key, ev.target.value);
    this.changeName = updateFieldEvent('name');
    this.changeDescription = updateFieldEvent('description');
    this.changeComponentImage = updateFieldEvent('componentImage');
    this.changewhereToFindItInput = updateFieldEvent('whereToFindItInput');

    this.watchForEnter = ev => {
      if (ev.keyCode === 13) {
        ev.preventDefault();
        this.props.onAddwhereToFindIt();
      }
    };

    this.removewhereToFindItHandler = whereToFindIt => () => {
      this.props.onRemovewhereToFindIt(whereToFindIt);
    };

    this.submitForm = ev => {
      ev.preventDefault();
      const component = {
        name: this.props.name,
        componentImage: this.props.componentImage,
        description: this.props.description,
        whereToFindItList: this.props.whereToFindItList
      };

      const slug = { slug: this.props.componentSlug };
      const promise = this.props.componentSlug ?
        agent.Components.update(Object.assign(component, slug)) :
        agent.Components.create(component);

      this.props.onSubmit(promise);
      this.setState({ redirectToNewPage: true })

    };
  }

  componentWillReceiveProps(nextProps) {
    if (this.props.match.params.slug !== nextProps.match.params.slug) {
      if (nextProps.match.params.slug) {
        this.props.onUnload();
        return this.props.onLoad(agent.Components.get(this.props.match.params.slug));
      }
      this.props.onLoad(null);
    }
  }

  componentWillMount() {
    if (this.props.match.params.slug) {
      return this.props.onLoad(agent.Components.get(this.props.match.params.slug));
    }
    this.props.onLoad(null);
  }

  componentWillUnmount() {
    this.props.onUnload();
  }

  render() {

    if (this.state.redirectToNewPage) {
      return (
      <Redirect to="/"/>
      )
    }

    return (
      <div className="editor-page">
        <div className="container page">
          <div className="row">
            <div className="col-md-10 offset-md-1 col-xs-12">

              <ListErrors errors={this.props.errors}></ListErrors>

              <form>
                <fieldset>

                  <fieldset className="form-group">
                    <input
                      className="form-control form-control-lg"
                      type="text"
                      placeholder="Nome do componente eletrônico"
                      value={this.props.name}
                      onChange={this.changeName} />
                  </fieldset>

                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Endereço para a imagem do componente"
                      value={this.props.componentImage}
                      onChange={this.changeComponentImage} />
                  </fieldset>

                  <fieldset className="form-group">
                    <textarea
                    className="form-control"
                    rows="8"
                      placeholder="Breve descrição do componente"
                      value={this.props.description}
                      onChange={this.changeDescription} />
                  </fieldset>

                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Onde podemos encontrar esse componente?"
                      value={this.props.whereToFindItInput}
                      onChange={this.changewhereToFindItInput}
                      onKeyUp={this.watchForEnter} />

                    <div className="tag-list">
                      {
                        (this.props.whereToFindItList || []).map(whereToFindIt => {
                          return (
                            <span className="tag-default tag-pill" key={whereToFindIt}>
                              <i  className="ion-close-round"
                                  onClick={this.removewhereToFindItHandler(whereToFindIt)}>
                              </i>
                              {whereToFindIt}
                            </span>
                          );
                        })
                      }
                    </div>
                  </fieldset>

                  <button
                    className="btn btn-lg pull-xs-right btn-primary"
                    type="button"
                    disabled={this.props.inProgress}
                    onClick={this.submitForm}>
                    Publicar Componente
                  </button>

                </fieldset>
              </form>

            </div>
          </div>
        </div>
      </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(EditorComponent);
