import * as React from 'react';

import './ObjectsList.scss';
import '../../../Shared/Styles/helpers.scss';

import Object from '../../Models/Object';
import ObjectsListSummary from '../ObjectsListSummary/ObjectsListSummary';
import ObjectsListItem from '../ObjectsListItem/ObjectsListItem';
import Button from '../../../Shared/Components/Button';

interface ObjectsListProps {
    objects: Object[]
}

class ProjectsList extends React.Component<ObjectsListProps, any> {

    private paths = {
        createProjectUrl: '/Objects/Add'
    };

    render() {
        return (
            <section className='ProjectsList row'>
                <div className='col-md-8 col-md-push-2'>
                    <ObjectsListSummary objects={this.props.objects} />
                    {
                        this.props.objects.map(object => {
                            return <ObjectsListItem key={object.Id} object={object} />
                        })
                    }
                </div>
            </section>
        )
    }
  
}

export default ProjectsList;