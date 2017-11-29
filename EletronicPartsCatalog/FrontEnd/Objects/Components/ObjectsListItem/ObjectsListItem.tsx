import * as React from 'react';
import * as moment from 'moment';

import './ObjectsListItem.scss';
import Object from '../../Models/Object';

interface ObjectListItemProps {
    key: number,
    object: Object
}

class ObjectsListItem extends React.Component<ObjectListItemProps, any> {

    render() {
        return (
            <a href={`/Objects/DetailsObj/${this.props.object.Id}`}>
                <div className='ObjectsListItem row'>
                    <div className='col-md-12'>
                        <div className='ObjectsListItem-summary row'>
                            <div className='col-md-6 padding-none'>
                                <span className='ObjectsListItem-name'>{this.props.object.Name}</span>
                            </div>
                            <div className='col-md-6 padding-none'>
                                <span className='pull-right'>
                                    <span className='glyphicon glyphicon-calendar'></span> {moment(this.props.object.CreationDate).format("DD-MM-YYYY")}
                                </span>
                            </div>
                        </div>
                        <div className='row'>
                            <div className='col-md-12 padding-none'>
                                <span className='ObjectsListItem-description'>{this.props.object.Description}</span>
                            </div>
                            <div className='row'>
                                <div className='col-md-12 padding-right'>
                                    <span className='ObjectsListItem-createdby'>{this.props.object.CreatedBy}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </a>
        )
    }

}

export default ObjectsListItem;

