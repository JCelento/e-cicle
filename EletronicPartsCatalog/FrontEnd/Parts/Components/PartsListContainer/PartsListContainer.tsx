import * as React from 'react';
import Part from '../../Models/Part';

import PartsList from '../PartsList/PartsList';
import EmptyListWarning from '../EmptyListWarning/EmptyListWarning';

interface PartsListContainerState {
    loadingData: boolean,
    parts: Part[]
}

class PartsListContainer extends React.Component<any, PartsListContainerState> {

    private paths = {
        fetchAllParts: '/api/Parts'
    };

    constructor() {
        super();

        this.state = {
            loadingData: true,
            parts: []
        };
    }

    public componentDidMount() {
        fetch(this.paths.fetchAllParts, { credentials: 'include' })
            .then((response) => {
                return response.text();
            })
            .then((data) => {
                this.setState((state, props) => {
                    state.parts = JSON.parse(data);
                    state.loadingData = false;
                });
            });
    }

    public render() {
        const hasParts = this.state.parts.length > 0;

        return (
            this.state.loadingData ?
               <p className='text-center'>Carregando...</p> :
               hasParts ? <PartsList parts={this.state.parts} /> : <EmptyListWarning />
        )
    }
}

export default PartsListContainer;