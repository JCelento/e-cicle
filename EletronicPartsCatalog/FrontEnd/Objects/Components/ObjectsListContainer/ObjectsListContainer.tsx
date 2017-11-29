import * as React from 'react';
import Object from '../../Models/Object';

import ObjectsList from '../ObjectsList/ObjectsList';
import EmptyListWarning from '../EmptyListWarning/EmptyListWarning';

interface ProjectsListContainerState {
    loadingData: boolean,
    objects: Object[]
}

class ObjectsListContainer extends React.Component<any, ProjectsListContainerState> {

    private paths = {
        fetchAllProjects: '/api/Objects'
    };

    constructor() {
        super();

        this.state = {
            loadingData: true,
            objects: []
        };
    }

    public componentDidMount() {
        fetch(this.paths.fetchAllProjects, { credentials: 'include' })
            .then((response) => {
                return response.text();
            })
            .then((data) => {
                this.setState((state, props) => {
                    state.objects = JSON.parse(data);
                    state.loadingData = false;
                });
            });
    }

    public render() {
        const hasProjects = this.state.objects.length > 0;

        return (
            this.state.loadingData ?
               <p className='text-center'>Carregando...</p> :
               hasProjects ? <ObjectsList objects={this.state.objects} /> : <EmptyListWarning />
        )
    }
}

export default ObjectsListContainer;