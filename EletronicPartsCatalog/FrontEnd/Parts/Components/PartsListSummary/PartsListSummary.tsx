import * as React from 'react';

import Part from '../../Models/Part';
import Button from '../../../Shared/Components/Button';

interface PartsListProps {
    parts: Part[]
}

class PartsListSummary extends React.Component<PartsListProps, any> {

    private paths = {
        createPartUrl: '/Parts/Add'
    };

    render() {
        return (
            <div className='row'>
                <div className='PartsList-summary col-md-12'>
                    <div className='row'>
                        <div className='col-md-6'>
                            <p className='PartList-summaryText'>Número de componentes cadastrados: <strong>{this.props.parts.length}</strong></p>
                        </div>
                        <div className='col-md-6'>
                            <Button link={true} href={this.paths.createPartUrl} extraClassNames={['btn-success', 'pull-right']}>Cadastrar componente</Button>
                        </div>
                    </div>
                </div>
            </div>
        )
    }

}

export default PartsListSummary;