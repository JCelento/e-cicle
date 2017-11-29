import * as React from 'react';

import './PartsList.scss';
import '../../../Shared/Styles/helpers.scss';

import Part from '../../Models/Part';
import PartsListSummary from '../PartsListSummary/PartsListSummary';
import PartsListItem from '../PartsListItem/PartsListItem';
import Button from '../../../Shared/Components/Button';

interface PartsListProps {
    parts: Part[]
}

class PartsList extends React.Component<PartsListProps, any> {

    private paths = {
        createPartUrl: '/Parts/Add'
    };

    render() {
        return (
            <section className='PartsList row'>
                <div className='col-md-8 col-md-push-2'>
                    <PartsListSummary parts={this.props.parts} />
                    {
                        this.props.parts.map(part => {
                            return <PartsListItem key={part.Id} part={part} />
                        })
                    }
                </div>
            </section>
        )
    }
  
}

export default PartsList;