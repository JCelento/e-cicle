import * as React from 'react';
import * as moment from 'moment';

import './PartsListItem.scss';
import Part from '../../Models/Part';

interface PartListItemProps {
    key: number,
    part: Part
}

class PartsListItem extends React.Component<PartListItemProps, any> {

    render() {
        return (
            <a href={`/Parts/DetailsObj/${this.props.part.Id}`}>
                <div className='PartsListItem row'>
                    <div className='col-md-12'>
                        <div className='PartsListItem-summary row'>
                            <div className='col-md-6 padding-none'>
                                <span className='PartsListItem-name'>{this.props.part.Name}</span>
                            </div>
                            <div className='col-md-6 padding-none'>
                                <span className='pull-right'>
                                    <span className='glyphicon glyphicon-calendar'></span> {moment(this.props.part.CreationDate).format("DD-MM-YYYY")}
                                </span>
                            </div>
                        </div>
                        <div className='row'>
                            <div className='col-md-12 padding-none'>
                                <span className='PartsListItem-description'>{this.props.part.Description}</span>
                            </div>
                            <div className='row'>
                                <div className='col-md-12 padding-right'>
                                    <span className='PartsListItem-createdby'>{this.props.part.CreatedBy}</span>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </a>
        )
    }

}

export default PartsListItem;

