import ListErrors from './ListErrors';
import React from 'react';
import agent from '../agent';
import { connect } from 'react-redux';
import {
  ADD_TAG,
  EDITOR_PAGE_LOADED,
  REMOVE_TAG,
  PROJECT_SUBMITTED,
  EDITOR_PAGE_UNLOADED,
  UPDATE_FIELD_EDITOR,
  ADD_COMPONENT,
  REMOVE_COMPONENT
} from '../constants/actionTypes';

const mapStateToProps = state => ({
  ...state.editor
});

const mapDispatchToProps = dispatch => ({
  onAddTag: () =>
    dispatch({ type: ADD_TAG }),
  onLoad: payload =>
    dispatch({ type: EDITOR_PAGE_LOADED, payload }),
  onRemoveTag: tag =>
    dispatch({ type: REMOVE_TAG, tag }),
  onAddComponent: () =>
    dispatch({ type: ADD_COMPONENT }),
  onRemoveComponent: component =>
    dispatch({ type: REMOVE_COMPONENT, component }),
  onSubmit: payload =>
    dispatch({ type: PROJECT_SUBMITTED, payload }),
  onUnload: payload =>
    dispatch({ type: EDITOR_PAGE_UNLOADED }),
  onUpdateField: (key, value) =>
    dispatch({ type: UPDATE_FIELD_EDITOR, key, value })
});

class Editor extends React.Component {

  constructor() {
    super();

    const updateFieldEvent =
      key => ev => this.props.onUpdateField(key, ev.target.value);
    this.changeTitle = updateFieldEvent('title');
    this.changeDescription = updateFieldEvent('description');
    this.changeBody = updateFieldEvent('body');
    this.changeTagInput = updateFieldEvent('tagInput');
    this.changeComponentInput = updateFieldEvent('componentInput');
    this.changeProjectImage = updateFieldEvent('projectImage');

    this.watchForEnterTag = ev => {
        ev.preventDefault();
        this.props.onAddTag();
    };

    this.removeTagHandler = tag => () => {
      this.props.onRemoveTag(tag);
    };

    this.watchForEnterComponent = ev => {
        ev.preventDefault();
        this.props.onAddComponent();
    };

    this.removeComponentHandler = component => () => {
      this.props.onRemoveComponent(component);
    };

    this.submitForm = ev => {
      ev.preventDefault();
      const project = {
        title: this.props.title,
        projectImage: this.props.projectImage,
        description: this.props.description,
        body: this.props.body,
        tagList: this.props.tagList,
        componentList: this.props.componentList
      };

      const slug = { slug: this.props.projectSlug };
      const promise = this.props.projectSlug ?
        agent.Projects.update(Object.assign(project, slug)) :
        agent.Projects.create(project);

      this.props.onSubmit(promise);
    };
  }

  componentWillReceiveProps(nextProps) {
    if (this.props.match.params.slug !== nextProps.match.params.slug) {
      if (nextProps.match.params.slug) {
        this.props.onUnload();
        return this.props.onLoad(agent.Projects.get(this.props.match.params.slug));
      }
      this.props.onLoad(null);
    }
  }

  componentWillMount() {
    if (this.props.match.params.slug) {
      return this.props.onLoad(agent.Projects.get(this.props.match.params.slug));
    }
    this.props.onLoad(null);
  }

  componentWillUnmount() {
    this.props.onUnload();
  }

  render() {

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
                      placeholder="Título*"
                      value={this.props.title}
                      onChange={this.changeTitle} />
                  </fieldset>

                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Endereço para a imagem do seu projeto"
                      value={this.props.projectImage}
                      onChange={this.changeProjectImage} />
                  </fieldset>

                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Breve descrição do seu projeto*"
                      value={this.props.description}
                      onChange={this.changeDescription} />
                  </fieldset>

                  <fieldset className="form-group">
                    <textarea
                      className="form-control"
                      rows="8"
                      placeholder="Escreva sobre o seu projeto (em markdown)"
                      value={this.props.body}
                      onChange={this.changeBody}>
                    </textarea>
                  </fieldset>

                  <button
                    className="btn pull-xs-right btn-primary"
                    type="button"
                    onClick={this.watchForEnterTag}>
                   Adicionar tag
                  </button>


                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Atribua tags*"
                      value={this.props.tagInput}
                      onChange={this.changeTagInput}
                      />

                    <div className="tag-list">
                      {
                        (this.props.tagList || []).map(tag => {
                          return (
                            <span className="tag-default tag-pill" key={tag}>
                              <i  className="ion-close-round"
                                  onClick={this.removeTagHandler(tag)}>
                              </i>
                              {tag}
                            </span>
                          );
                        })
                      }
                    </div>
                  </fieldset>

                  <button
                    className="btn pull-xs-right btn-primary"
                    type="button"
                    onClick={this.watchForEnterComponent}>
                   Adicionar Componente
                  </button>

                  <fieldset className="form-group">
                    <input
                      className="form-control"
                      type="text"
                      placeholder="Quais componentes eletronicos utilizou?*"
                      value={this.props.componentInput}
                      onChange={this.changeComponentInput}
                       />

                    <div className="tag-list">
                      {
                        (this.props.componentList || []).map(component => {
                          return (
                            <span className="tag-default tag-pill tag-primary" key={component}>
                              <i  className="ion-close-round"
                                  onClick={this.removeComponentHandler(component)}>
                              </i>
                              {component}
                            </span>
                          );
                        })
                      }
                    </div>
                  </fieldset>

                  <button
                    className="btn btn-lg pull-xs-right btn-primary"
                    type="button"
                    onClick={this.submitForm}>
                    Publicar Projeto
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

export default connect(mapStateToProps, mapDispatchToProps)(Editor);
