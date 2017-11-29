import * as React from 'react';

import Object from '../../Models/Object';
import Button from '../../../Shared/Components/Button';

interface ProjectsListProps {
    objects: Object[]
}

class ObjectsListSummary extends React.Component<ProjectsListProps, any> {

    private paths = {
        createProjectUrl: '/Objects/Add'
    };

    render() {
        return (
            <div className='row'>
                <div className='ProjectsList-summary col-md-12'>
                    <div className='row'>
                        <div className='col-md-6'>
                            <p className='ProjectList-summaryText'>Número de E-Wastes cadastrados: <strong>{this.props.objects.length}</strong></p>
                        </div>
                        <div className='col-md-6'>
                            <Button link={true} href={this.paths.createProjectUrl} extraClassNames={['btn-success', 'pull-right']}>Cadastrar e-waste</Button>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

}

export default ObjectsListSummary;