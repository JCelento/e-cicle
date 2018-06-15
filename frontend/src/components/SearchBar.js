import React from 'react';
import agent from '../agent';
import { connect } from 'react-redux';
import {
  UPDATE_FIELD_SEARCH,
  SEARCH,
  APPLY_SEARCH_FILTER
} from '../constants/actionTypes';

const mapStateToProps = state => ({ ...state.common});

const mapDispatchToProps = dispatch => ({
  onChangeSearch: value =>
    dispatch({ type: UPDATE_FIELD_SEARCH, key: 'search', value }),
  onSubmit: (search) => {
    const payload = agent.Projects.bySearch(search);
    dispatch({ type: APPLY_SEARCH_FILTER, payload })
  }
});

class SearchBar extends React.Component {
  constructor() {
    super();
    this.changeSearch = ev => this.props.onChangeSearch(ev.target.value);
    this.submitForm = (search) => ev => {
      ev.preventDefault();
      this.props.onSubmit(search);
    }
  }

  render() {
    const search = this.props.search;

    return (
      <div>
              <form onSubmit={this.submitForm(search)}>
                <fieldset>
                  <fieldset className="form-group">
                    <input
                      className="form-control form-control-lg"
                      type="text"
                      placeholder="O que procura?"
                      value={search}
                      onChange={this.changeSearch} />
                  </fieldset>
                  <button
                    className="btn btn-lg btn-primary pull-xs-right"
                    type="submit"
                    disabled={this.props.inProgress}>
                    Buscar
                  </button>

                </fieldset>
              </form>
            </div>
    );
  }
}

export default connect(mapStateToProps, mapDispatchToProps)(SearchBar);
