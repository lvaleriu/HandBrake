/* hb.h

   Copyright (c) 2003-2015 HandBrake Team
   This file is part of the HandBrake source code
   Homepage: <http://handbrake.fr/>.
   It may be used under the terms of the GNU General Public License v2.
   For full terms see the file COPYING file or visit http://www.gnu.org/licenses/gpl-2.0.html
 */
 
#ifndef HB_HB_H
#define HB_HB_H

#ifdef __cplusplus
extern "C" {
#endif

#include "common.h"
#include "project.h"
#include "compat.h"
#include "hb_dict.h"
#include "hb_json.h"
#include "preset.h"
#include "plist.h"
#include "param.h"

/* hb_init()
   Initializes a libhb session (launches his own thread, detects CPUs,
   etc) */
#define HB_DEBUG_NONE 0
#define HB_DEBUG_ALL  1
void          hb_register( hb_work_object_t * );
void          hb_register_logger( void (*log_cb)(const char* message) );
hb_handle_t * hb_init( int verbose, int update_check );
void          hb_update_poll(hb_handle_t *h);
void          hb_log_level_set(hb_handle_t *h, int level);

void          hb_hwd_set_enable( hb_handle_t *h, uint8_t enable );
int           hb_hwd_enabled( hb_handle_t *h );
hb_hwd_t    * hb_hwd_get_context();

/* hb_get_version() */
char        * hb_get_version( hb_handle_t * );
int           hb_get_build( hb_handle_t * );

/* hb_check_update()
   Checks for an update on the website. If there is, returns the build
   number and points 'version' to a version description. Returns a
   negative value otherwise. */
int           hb_check_update( hb_handle_t * h, char ** version );


char *        hb_dvd_name( char * path );
void          hb_dvd_set_dvdnav( int enable );

/* hb_scan()
   Scan the specified path. Can be a DVD device, a VIDEO_TS folder or
   a VOB file. If title_index is 0, scan all titles. */
void          hb_scan( hb_handle_t *, const char * path,
                       int title_index, int preview_count,
                       int store_previews, uint64_t min_duration );
void          hb_scan_stop( hb_handle_t * );
uint64_t      hb_first_duration( hb_handle_t * );

/* hb_get_titles()
   Returns the list of valid titles detected by the latest scan. */
hb_list_t   * hb_get_titles( hb_handle_t * );

/* hb_get_title_set()
   Returns the title set which contains a list of valid titles detected
   by the latest scan and title set data. */
hb_title_set_t   * hb_get_title_set( hb_handle_t * );

/* hb_detect_comb()
   Analyze a frame for interlacing artifacts, returns true if they're found.
   Taken from Thomas Oestreich's 32detect filter in the Transcode project.  */
int hb_detect_comb( hb_buffer_t * buf, int color_equal, int color_diff, int threshold, int prog_equal, int prog_diff, int prog_threshold );

// JJJ: title->job?
int           hb_save_preview( hb_handle_t * h, int title, int preview, 
                               hb_buffer_t *buf );
hb_buffer_t * hb_read_preview( hb_handle_t * h, hb_title_t *title,
                               int preview );
hb_image_t  * hb_get_preview2(hb_handle_t * h, int title_idx, int picture,
                              hb_geometry_settings_t *geo, int deinterlace);
void          hb_set_anamorphic_size2(hb_geometry_t *src_geo,
                                      hb_geometry_settings_t *geo,
                                      hb_geometry_t *result);
void          hb_add_filter( hb_job_t * job, hb_filter_object_t * filter, 
                const char * settings );

/* Handling jobs */
int           hb_count( hb_handle_t * );
hb_job_t    * hb_job( hb_handle_t *, int );
void          hb_add( hb_handle_t *, hb_job_t * );
void          hb_rem( hb_handle_t *, hb_job_t * );

hb_title_t  * hb_find_title_by_index( hb_handle_t *h, int title_index );
hb_job_t    * hb_job_init_by_index( hb_handle_t *h, int title_index );
hb_job_t    * hb_job_init( hb_title_t * title );
void          hb_job_close( hb_job_t ** job );

void          hb_start( hb_handle_t * );
void          hb_pause( hb_handle_t * );
void          hb_resume( hb_handle_t * );
void          hb_stop( hb_handle_t * );

void          hb_system_sleep_allow(hb_handle_t*);
void          hb_system_sleep_prevent(hb_handle_t*);

/* Persistent data between jobs. */
typedef struct hb_interjob_s
{
    int last_job;          /* job->sequence_id & 0xFFFFFF */
    int frame_count;       /* number of frames counted by sync */
    int out_frame_count;   /* number of frames counted by render */
    uint64_t total_time;   /* real length in 90kHz ticks (i.e. seconds / 90000) */
    hb_rational_t vrate;   /* actual measured output vrate from 1st pass */

    hb_subtitle_t *select_subtitle; /* foreign language scan subtitle */
} hb_interjob_t;

hb_interjob_t * hb_interjob_get( hb_handle_t * ); 

/* hb_get_state()
   Should be regularly called by the UI (like 5 or 10 times a second).
   Look at test/test.c to see how to use it. */
void hb_get_state( hb_handle_t *, hb_state_t * );
void hb_get_state2( hb_handle_t *, hb_state_t * );

/* hb_close()
   Aborts all current jobs if any, frees memory. */
void          hb_close( hb_handle_t ** );

/* hb_global_init()
   Performs process initialization. */
int           hb_global_init();
/* hb_global_close()
   Performs final cleanup for the process. */
void          hb_global_close();

/* hb_get_instance_id()
   Return the unique instance id of an libhb instance created by hb_init. */
int hb_get_instance_id( hb_handle_t * h );

#ifdef __cplusplus
}
#endif

#endif
